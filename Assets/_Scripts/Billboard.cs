using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;    
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + _cam.transform.forward);        
    }
}
