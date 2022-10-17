using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMan : MonoBehaviour
{
    Rigidbody rb;
  [HideInInspector]  public float x, y, z;
    public bool rot;
    public bool right;
    public float zRot;
    public float changeRot;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        x = Random.Range(-5, 5);
        y = Random.Range(-5, 5);
        z = Random.Range(-5, 5);
        changeRot = Random.Range(0.33f, 0.77f);
    }
    private void FixedUpdate()
    {
        if (rot)
        {
            transform.Rotate(x, y, z);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, zRot);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GiantParent")
        {
            rot = true;
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(Random.Range(-500, 500), Random.Range(200, 500), Random.Range(-500, 500));
        }
    }
}
