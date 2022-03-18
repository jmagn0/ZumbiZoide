using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoJogador : MovimentoPersonagem
{
    public void RotacaoJogador(LayerMask mascaraChao)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, mascaraChao))
        {
            Vector3 posicaoMiraJogador = hit.point - transform.position;
            posicaoMiraJogador.y = transform.position.y;

            Rotacionar(posicaoMiraJogador);
        }
    }
}
