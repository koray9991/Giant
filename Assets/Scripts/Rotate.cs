using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public static Rotate instance;
    public float x, y, z;
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(x, y, z);
    }
}
