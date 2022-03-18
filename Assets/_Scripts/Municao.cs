using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municao : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _velocidade = 20f;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();    
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + transform.forward * Time.deltaTime * _velocidade);
    }

    private void OnTriggerEnter(Collider other)
    {
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward); // Poderia pegar a normal
        if (other.CompareTag(Tags.INIMIGO))
        {
            
            IMatavel inimigo = other.GetComponent<IMatavel>();
            inimigo.TomarDano(1);
            inimigo.ParticulaSangue(transform.position, rotacaoOpostaABala);

        }

        Destroy(gameObject);
    }
}
