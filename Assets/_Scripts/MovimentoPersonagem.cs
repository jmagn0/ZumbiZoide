using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    // Cache
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        _rb.MovePosition(_rb.position + (direcao.normalized * Time.deltaTime * velocidade));
    }

    public void Rotacionar(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _rb.MoveRotation(lookRotation);
    }

    public void Morrer()
    {
        _rb.constraints = RigidbodyConstraints.None;
        _rb.velocity = Vector3.zero;
    }
}
