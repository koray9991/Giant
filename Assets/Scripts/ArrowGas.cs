using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGas : MonoBehaviour
{
    public Rigidbody rb;
    bool up;
    public float speed;
    public float timer, spawnTime;
   
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime && timer<spawnTime+0.1f)
        {
           transform.GetChild(0).gameObject.SetActive(true);
        }

    }
    void FixedUpdate()
    {
        if (up)
        {
            rb.velocity = new Vector3(speed, -speed, 0);
        }
        else
        {
            rb.velocity = new Vector3(-speed, speed, 0);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "down")
        {
            up = false;
        }
        if (other.tag == "up")
        {
            up = true;
        }
    }
}
