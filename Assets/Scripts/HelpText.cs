using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpText : MonoBehaviour
{
    SpriteRenderer sr;
    float alpha;
    public bool right;
    public float zRot;
    public float changeRot;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        alpha = 1;
        changeRot = Random.Range(0.33f, 0.77f);
    }

    void FixedUpdate()
    {
        alpha -= 0.006f;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        transform.rotation = Quaternion.Euler(0, 0, zRot);
        if (right)
        {
            zRot += changeRot;
            if (zRot > 10)
            {
                right = false;
            }
        }
        else
        {
            zRot -= changeRot;
            if (zRot < -10)
            {
                right = true;
            }
        }
            
    }
}
