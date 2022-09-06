using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoBehaviour
{
    public Rigidbody rb;
    public float startSpeed;
    float velocity;
    public static Giant instance;
    public GameObject mesh;
    public Material[] mat;
    bool laserEffect;
    private void Awake()
    {
        if (instance == null) instance = this;
    }


    private void FixedUpdate()
    {
        float ropeCount = GameObject.FindGameObjectsWithTag("RopeToGiant").Length;
        velocity = startSpeed - ropeCount / 2;
        if (velocity <= 0)
        {
            velocity = 0;
           
        }
        if (laserEffect)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, velocity/4);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, velocity);
        }
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Laser")
        {
            mesh.GetComponent<SkinnedMeshRenderer>().material = mat[1];
            laserEffect = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Laser")
        {
            mesh.GetComponent<SkinnedMeshRenderer>().material = mat[0];
            laserEffect = false;
        }
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
