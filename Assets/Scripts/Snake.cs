using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Rigidbody rb;
    public List<GameObject> heads;
    float zSpeed;
    void Start()
    {
        zSpeed = 4;
    }

    
    void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, zSpeed);
        transform.position = new Vector3(0, 0, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            zSpeed = 0;
            GetComponent<Animator>().SetInteger("movement", 1);
            //   GetComponent<BoxCollider>().size = new Vector3(1, 1.3f, 1);
            
            for (int i = 0; i < heads.Count; i++)
            {
                heads[i].transform.parent = null;
                heads[i].AddComponent<Rigidbody>();
                heads[i].AddComponent<BoxCollider>();
            }
        }
    }
}
