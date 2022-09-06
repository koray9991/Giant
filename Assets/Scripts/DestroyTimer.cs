using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{  
    [HideInInspector]
    public float timer;
    public float DestroyTime;
    void Start()
    {
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > DestroyTime)
        {
            Destroy(gameObject);
        }
    }
}
