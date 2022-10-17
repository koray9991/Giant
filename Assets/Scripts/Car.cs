using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject tyre1, tyre2, tyre3, tyre4,explosionParticle;
    public bool MainBool;
    public bool getBigger;
    public float timer;
    public float currentX, currentY, currentZ;
    public float scaleIndex;
    public float maxHealth;
    public float health;
   
    void Update()
    {
        if (MainBool)
        {
            getBigger = true;
            timer += Time.deltaTime;
            if (timer > 0.05f)
            {
                getBigger = false;
            }
            if (timer > 0.1f)
            {
                MainBool = false;
                timer = 0;

            }
            if (getBigger == true)
            {
                transform.localScale = new Vector3(transform.transform.localScale.x + scaleIndex, transform.transform.localScale.y + scaleIndex, transform.transform.localScale.z + scaleIndex);
            }
            else
            {
                transform.localScale = new Vector3(transform.transform.localScale.x - scaleIndex, transform.transform.localScale.y - scaleIndex, transform.transform.localScale.z - scaleIndex);
            }
        }
        else
        {
            if (transform.localScale.x != currentX)
            {
                transform.localScale = new Vector3(currentX, currentY, currentZ);
            }

        }


        if (health <= 0 && gameObject.layer!=9)
        {
            gameObject.layer = 9;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddForce(Random.Range(-300, 300), Random.Range(300, 500), Random.Range(0, 400));
            explosionParticle.GetComponent<ParticleSystem>().Play();


            tyre1.transform.parent = null;
            tyre2.transform.parent = null;
            tyre3.transform.parent = null;
            tyre4.transform.parent = null;

            tyre1.layer = 9;
            tyre2.layer = 9;
            tyre3.layer = 9;
            tyre4.layer = 9;

            if (tyre1.GetComponent<Rigidbody>() == null)
            {
                tyre1.AddComponent<Rigidbody>();
            }
            if (tyre2.GetComponent<Rigidbody>() == null)
            {
                tyre2.AddComponent<Rigidbody>();
            }
            if (tyre3.GetComponent<Rigidbody>() == null)
            {
                tyre3.AddComponent<Rigidbody>();
            }
            if (tyre4.GetComponent<Rigidbody>() == null)
            {
                tyre4.AddComponent<Rigidbody>();
            }


            tyre1.AddComponent<BoxCollider>();
            tyre2.AddComponent<BoxCollider>();
            tyre3.AddComponent<BoxCollider>();
            tyre4.AddComponent<BoxCollider>();

            tyre1.GetComponent<Rigidbody>().AddForce(Random.Range(-500, 500), Random.Range(300, 500), Random.Range(-400, 400));
            tyre2.GetComponent<Rigidbody>().AddForce(Random.Range(-500, 500), Random.Range(300, 500), Random.Range(-400, 400));
            tyre3.GetComponent<Rigidbody>().AddForce(Random.Range(-500, 500), Random.Range(300, 500), Random.Range(-400, 400));
            tyre4.GetComponent<Rigidbody>().AddForce(Random.Range(-500, 500), Random.Range(300, 500), Random.Range(-400, 400));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Giant")
        {
            gameObject.layer = 9;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddForce(Random.Range(-300, 300), Random.Range(300, 500), Random.Range(0, 400));
            explosionParticle.GetComponent<ParticleSystem>().Play();


            tyre1.transform.parent = null;
            tyre2.transform.parent = null;
            tyre3.transform.parent = null;
            tyre4.transform.parent = null;

            tyre1.layer = 9;
            tyre2.layer = 9;
            tyre3.layer = 9;
            tyre4.layer = 9;

            if (tyre1.GetComponent<Rigidbody>() == null)
            {
                tyre1.AddComponent<Rigidbody>();
            }
            if (tyre2.GetComponent<Rigidbody>() == null)
            {
                tyre2.AddComponent<Rigidbody>();
            }
            if (tyre3.GetComponent<Rigidbody>() == null)
            {
                tyre3.AddComponent<Rigidbody>();
            }
            if (tyre4.GetComponent<Rigidbody>() == null)
            {
                tyre4.AddComponent<Rigidbody>();
            }
          

            tyre1.AddComponent<BoxCollider>();
            tyre2.AddComponent<BoxCollider>();
            tyre3.AddComponent<BoxCollider>();
            tyre4.AddComponent<BoxCollider>();

            tyre1.GetComponent<Rigidbody>().AddForce(Random.Range(-500, 500), Random.Range(300, 500), Random.Range(-400, 400));
            tyre2.GetComponent<Rigidbody>().AddForce(Random.Range(-500, 500), Random.Range(300, 500), Random.Range(-400, 400));
            tyre3.GetComponent<Rigidbody>().AddForce(Random.Range(-500, 500), Random.Range(300, 500), Random.Range(-400, 400));
            tyre4.GetComponent<Rigidbody>().AddForce(Random.Range(-500, 500), Random.Range(300, 500), Random.Range(-400, 400));
        }
    }
}
