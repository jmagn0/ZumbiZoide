using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    private GameObject _jogador;
    private Vector3 _offset;

    void Start()
    {
        _jogador = GameObject.FindGameObjectWithTag(Tags.JOGADOR);
        _offset = transform.position - _jogador.transform.position;
    }

    void LateUpdate()
    {
        transform.position = _jogador.transform.position + _offset;
    }
}
