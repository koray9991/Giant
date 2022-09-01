using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoBehaviour
{
    public Rigidbody rb;
    public float z;
    float velocity;
    
    private void FixedUpdate()
    {
        float ropeCount = GameObject.FindGameObjectsWithTag("RopeToGiant").Length;
        velocity = z - ropeCount / 2;
        if (velocity <= 0)
        {
            velocity = 0;
            Debug.Log("Win");
        }
        rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y, velocity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destroyable")
        {
            other.GetComponent<Rigidbody>().AddForce(Random.Range(-300, 300), Random.Range(100, 400), Random.Range(200, 400));
            other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<BoxCollider>().isTrigger = false;
            other.transform.gameObject.layer = 9;
        }

        
    }
}
