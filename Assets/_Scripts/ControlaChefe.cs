using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    private NavMeshAgent _agent;
    private ControlaJogador _jogador;
    private Status _status;
    private AnimacaoPersonagem _anim;
    private MovimentoPersonagem _movimento;
    private ControlaInterface _interface;
    [SerializeField] private GameObject _kitMedicoPrefab;
    [SerializeField] private Slider _sliderVida;
    [SerializeField] private Image _imagemSlider;
    [SerializeField] private Color _corVidaMaxima;
    [SerializeField] private Color _corVidaMinima;
    [SerializeField] private GameObject _particulaSangue;

    public void Awake()
    {
        _anim = GetComponent<AnimacaoPersonagem>();
        _agent = GetComponent<NavMeshAgent>();        
        _status = GetComponent<Status>();
        _jogador = GameObject.FindWithTag(Tags.JOGADOR).GetComponent<ControlaJogador>();
        _interface = GameObject.FindWithTag(Tags.INTERFACE).GetComponent<ControlaInterface>();
        _movimento = GetComponent<MovimentoPersonagem>();        
    }
    private void Start()
    {
        _agent.speed = _status.Velocidade;
        _sliderVida.maxValue = _status.VidaInicial;
        AtualizarVida();
    }

    private void Update()
    {
        _agent.destination = _jogador.transform.position;

        if(!_agent.hasPath) { return; }

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _anim.Atacar(true);
            Vector3 direcao = _jogador.transform.position - transform.position;
            _movimento.Rotacionar(direcao);
        }
        else
        {
            _anim.Atacar(false);
            _anim.Movimentar(_agent.velocity.magnitude);
        }
    }

    public void AtacaJogador()
    {
        int dano = Random.Range(30, 41);
        _jogador.TomarDano(dano);
    }

    public void Morrer()
    {
        _anim.Morrer();
        _movimento.Morrer();
        _agent.enabled = false;
        this.enabled = false;
        Destroy(gameObject, 2f);
        Instantiate(_kitMedicoPrefab, transform.position, Quaternion.identity);
        _interface.AtualizarNumeroZumbisMortos();
    }

    public void TomarDano(int dano)
    {
        _status.Vida = Mathf.Max(_status.Vida - dano, 0);
        AtualizarVida();
        if(_status.Vida <= 0)
        {
            Morrer();
        }
    }

    void AtualizarVida()
    {
        _sliderVida.value = _status.Vida;
        float porcentagem = (float)_status.Vida / _status.VidaInicial;
        Color corAtualVida = Color.Lerp(_corVidaMinima, _corVidaMaxima, porcentagem);
        _imagemSlider.color = corAtualVida;
    }

    public void ParticulaSangue(Vector3 position, Quaternion rotation)
    {
        Instantiate(_particulaSangue, position, rotation);
    }
}
