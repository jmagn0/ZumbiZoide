using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Atacar(bool estado)
    {
        _anim.SetBool("atacar", estado);
    }

    public void Movimentar(float valorMovimento)
    {
        _anim.SetFloat("movendo", valorMovimento);
    }

    public void Morrer()
    {
        _anim.SetTrigger("morrer");
    }
        
    
}
