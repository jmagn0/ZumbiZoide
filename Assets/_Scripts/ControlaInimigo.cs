using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    // Cached
    private ControlaJogador _jogador;
    private MovimentoPersonagem _movimentoScript;
    private AnimacaoPersonagem _animacaoScript;
    private ControlaInterface _interface;
    private Status _status;
    private Vector3 _posicaoAleatoria;
    private Vector3 _direcao;
    private float _contadorVagar;
    [SerializeField] private AudioClip _somMorte;
    [SerializeField] private GameObject _kitMedicoPrefab;
    [SerializeField] private GameObject _particlePrefab;
    public GeradorZumbis Gerador;

    // State 
    [SerializeField] private float _tempoEntrePosicoesAleatorias = 4f;
    [SerializeField] private int _danoDoZumbi = 0;
    [SerializeField] private float _porcentagemDrop = 0.1f;


    private void Awake()
    {
        _jogador = GameObject.FindWithTag(Tags.JOGADOR).GetComponent<ControlaJogador>();
        _animacaoScript = GetComponent<AnimacaoPersonagem>();
        _movimentoScript = GetComponent<MovimentoPersonagem>();
        _status = GetComponent<Status>();
        _interface = GameObject.FindWithTag(Tags.INTERFACE).GetComponent<ControlaInterface>();
    }

    void Start() => AleatorizarZumbi();

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, _jogador.transform.position);

        _animacaoScript.Movimentar(_direcao.magnitude);
        _movimentoScript.Rotacionar(_direcao);

        if(distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5f)
        {
            Perseguir();
        }
        else
        {
            _direcao = _jogador.transform.position - transform.position;
            _animacaoScript.Atacar(true);
        }
    }

    void Perseguir()
    {
        _direcao = _jogador.transform.position - transform.position;
        _movimentoScript.Movimentar(_direcao, _status.Velocidade);
        _animacaoScript.Atacar(false);
    }

    public void AtacaJogador()
    {
        if(_danoDoZumbi <= 0)
        {
            int novoDano = Random.Range(20, 30);
            _jogador.TomarDano(novoDano);
            return;
        }

        _jogador.TomarDano(_danoDoZumbi);
    }

    void Vagar()
    {
        _contadorVagar -= Time.deltaTime;
        if(_contadorVagar <= 0)
        {
            _posicaoAleatoria = CriarPosicaoAleatoria();
            _contadorVagar = _tempoEntrePosicoesAleatorias + Random.Range(-1f,1f);
        }

        bool estaPertoDaPosicao = Vector3.Distance(transform.position, _posicaoAleatoria) <= 0.05;

        if (!estaPertoDaPosicao)
        {
            _direcao = _posicaoAleatoria - transform.position;
            _movimentoScript.Movimentar(_direcao, _status.Velocidade);
        }
    }

    Vector3 CriarPosicaoAleatoria()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
    }

    public void AleatorizarZumbi()
    {
        int numeroAleatorio = Random.Range(1, transform.childCount);
        transform.GetChild(numeroAleatorio).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        _status.Vida -= dano;        
        if(_status.Vida <= 0)
        {
            Morrer();
        }        
    }

    public void ParticulaSangue(Vector3 position, Quaternion rotation)
    {
        Instantiate(_particlePrefab, position, rotation);
    }

    public void Morrer()
    {
        ControlaAudio.Instance.PlayOneShot(_somMorte);
        VerificarDropKitMedico(_porcentagemDrop);
        _interface.AtualizarNumeroZumbisMortos();
        Gerador.DiminuirQuantidadeZumbis();
        _animacaoScript.Morrer();
        this.enabled = false;
        _movimentoScript.Morrer();
        Destroy(gameObject, 2f);
    }

    private void VerificarDropKitMedico(float porcentagem)
    {
        if(Random.value <= porcentagem)
        {
            Instantiate(_kitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}
