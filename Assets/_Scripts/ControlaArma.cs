using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    // State
    [SerializeField] private GameObject _municao;
    [SerializeField] private Transform _canoDaArma;
    [SerializeField] private AudioClip _somTiro;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_municao, _canoDaArma.position, _canoDaArma.rotation);
            ControlaAudio.Instance.PlayOneShot(_somTiro);
        }
    }
}
