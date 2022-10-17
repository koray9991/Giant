using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public Vector3 hitPoint;
    public float speed;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce((hitPoint - transform.position).normalized * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
