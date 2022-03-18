using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatavel 
{
    public void TomarDano(int dano);

    public void Morrer();

    public void ParticulaSangue(Vector3 position, Quaternion rotation);
}
