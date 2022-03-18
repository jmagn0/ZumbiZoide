using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int VidaInicial = 100;
    [HideInInspector] public int Vida;
    public float Velocidade = 5f;

    private void Awake()
    {
        Vida = VidaInicial;
    }

    private void Start()
    {
        
    }
}
