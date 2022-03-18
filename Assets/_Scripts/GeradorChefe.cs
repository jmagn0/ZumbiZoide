using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeradorChefe : MonoBehaviour
{
    private float _tempoParaProximoRespawn = 0f;
    [SerializeField] private float _tempoEntreRespawn = 60;
    [SerializeField] private GameObject _chefePrefab;
    [SerializeField] private Transform[] _posicoesGeracao;
    private ControlaInterface _interface;
    private ControlaJogador _jogador;

    private void Start()
    {
        _jogador = GameObject.FindWithTag(Tags.JOGADOR).GetComponent<ControlaJogador>();
        _tempoParaProximoRespawn = _tempoEntreRespawn;
        _interface = GameObject.FindWithTag(Tags.INTERFACE).GetComponent<ControlaInterface>();
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad > _tempoParaProximoRespawn)
        {
            Instantiate(_chefePrefab, PosicaoMaisDistanteJogador(), Quaternion.identity);
            _tempoParaProximoRespawn = Time.timeSinceLevelLoad + _tempoEntreRespawn;
            _interface.MostrarTextoChefeCriado();
        }
    }

    private Vector3 PosicaoMaisDistanteJogador()
    {
        Vector3 maiorPosicao = Vector3.zero;
        float maiorDistancia = 0;

        foreach (var posicao in _posicoesGeracao)
        {
            float distanciaEntreJogador = Vector3.Distance(posicao.position, _jogador.transform.position);
            if(distanciaEntreJogador > maiorDistancia)
            {
                maiorPosicao = posicao.position;
                maiorDistancia = distanciaEntreJogador;
            }
        }

        return maiorPosicao;
    }
}
