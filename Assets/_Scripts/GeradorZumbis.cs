using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    [SerializeField] private ControlaInimigo _zumbiPrefab;
    [SerializeField] private Transform _zumbiParent;
    [SerializeField] float _tempoGerarZumbi;
    private float _contadorTempo = 0;
    [SerializeField] LayerMask _layerZumbi;
    [SerializeField] private float _distanciaGerarZumbi = 3f;
    [SerializeField] private float _distanciaDoJogadorParaGeracao = 20f;
    private ControlaJogador _jogador;
    private int _quantidadeMaximaZumbis = 2;
    private int _quantidadeAtualZumbis = 0;
    private float _tempoProximoAumentoDificultade = 25;
    private float _contadorAumentarDificuldade = 0f;

    private void Start()
    {
        _jogador = GameObject.FindWithTag(Tags.JOGADOR).GetComponent<ControlaJogador>();
        _contadorAumentarDificuldade = _tempoProximoAumentoDificultade;
        for (int i = 0; i < _quantidadeMaximaZumbis; i++)
        {
            StartCoroutine(GerarNovoZumbi());
        }
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad >= _contadorAumentarDificuldade)
        {
            _quantidadeMaximaZumbis++;
            _contadorAumentarDificuldade = Time.timeSinceLevelLoad + _tempoProximoAumentoDificultade;
        }

        if(Vector3.Distance(transform.position, _jogador.transform.position) < _distanciaDoJogadorParaGeracao) { return; }
        if(_quantidadeAtualZumbis >= _quantidadeMaximaZumbis) { return; }

        _contadorTempo += Time.deltaTime;

        if( _contadorTempo >= _tempoGerarZumbi)
        {
            StartCoroutine(GerarNovoZumbi());
            _contadorTempo = 0;
        }            
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicao;
        Collider[] colisores;

        do
        {
            posicao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicao, 1, _layerZumbi);
            yield return null;
        } while (colisores.Length > 0);

        ControlaInimigo obj = Instantiate(_zumbiPrefab, posicao, transform.rotation, _zumbiParent);
        obj.Gerador = this;
        _quantidadeAtualZumbis++;
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicaoAleatoria = Random.insideUnitSphere * _distanciaGerarZumbi;
        posicaoAleatoria += transform.position;
        posicaoAleatoria.y = 0;

        return posicaoAleatoria;
    }

    public void DiminuirQuantidadeZumbis()
    {
        _quantidadeAtualZumbis--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _distanciaGerarZumbi);
    }
}
