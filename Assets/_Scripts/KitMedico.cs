using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int _quantidadeCura = 15;
    private int _tempoDestruicao = 8;

    private void Start()
    {
        Destroy(gameObject, _tempoDestruicao);            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.JOGADOR))
        {
            other.GetComponent<ControlaJogador>().Curar(_quantidadeCura);
            Destroy(gameObject);
        }
    }
}
