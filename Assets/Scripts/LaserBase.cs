using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBase : MonoBehaviour
{
   
    float g,b;
    bool rUp, rDown;
    void Start()
    {
       
        rUp = true;
    }

   
    void Update()
    {

        GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1,g,b));
        if (rUp)
        {
            g += 0.05f;
            b += 0.05f;
        }
        if (rDown)
        {
            g -= 0.05f;
            b -= 0.05f;
        }
        if (g >= 1)
        {
            rDown = true;
            rUp = false;
        }
        if (g <= 0)
        {
            rDown = false;
            rUp = true;
        }
    }
}
