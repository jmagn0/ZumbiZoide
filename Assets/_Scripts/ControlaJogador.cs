using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    private Vector3 _direcao;
    [SerializeField] private LayerMask _mascaraChao;
    [SerializeField] private GameObject TextoGameOver;
    [SerializeField] private ControlaInterface _interface;
    [SerializeField] private AudioClip _somDano;
    private MovimentoJogador _movimentoScript;
    private AnimacaoPersonagem _animacaoScript;
    private Status _status;

    // Start is called before the first frame update
    void Start()
    {
        _movimentoScript = GetComponent<MovimentoJogador>();
        _animacaoScript = GetComponent<AnimacaoPersonagem>();
        _status = GetComponent<Status>();
        _interface.AtualizarVida(_status.VidaInicial);
    }

    // Update is called once per frame
    void Update()
    {
        float eixoZ = Input.GetAxisRaw("Vertical");
        float eixoX = Input.GetAxisRaw("Horizontal");        
            
        _direcao = new Vector3(eixoX, 0, eixoZ);

        _animacaoScript.Movimentar(_direcao.magnitude);
        
    }

    private void FixedUpdate()
    {
        _movimentoScript.Movimentar(_direcao, _status.Velocidade);

        _movimentoScript.RotacaoJogador(_mascaraChao);
    }

    public void TomarDano(int dano)
    {
        _status.Vida -= dano;
        _interface.AtualizarVida(_status.Vida);
        ControlaAudio.Instance.PlayOneShot(_somDano);

        if(_status.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        _interface.GameOver();
    }

    public void Curar(int quantidade)
    {
        _status.Vida = Mathf.Min(_status.Vida + quantidade, _status.VidaInicial);
        _interface.AtualizarVida(_status.Vida);
    }

    public void ParticulaSangue(Vector3 position, Quaternion rotation)
    {
        // TODO - Jorrar sangue quando acertado pelo zumbi
    }
}
