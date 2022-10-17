using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Tanker : MonoBehaviour
{
    public static Tanker instance;
    public Rigidbody rb;
    public bool MainBool;
    public bool getBigger;
    public float timer;
    public float speed;
    public float currentX, currentY, currentZ;
    public float scaleIndex;

    public float maxHealth;
    public float health;
    public GameObject arrow;
    public GameObject explosion,explosionCollider;
  //  public GameObject canvas;
    
    
    public GameObject[] tyres;
  //  public GameObject damageText;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        health = maxHealth;
       
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, speed);
    }
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
        if (health <= 0)
        {
           // canvas.transform.parent = null;
           //var newText= Instantiate(damageText, transform.position, Quaternion.Euler(0, 0, 0));
           // newText.transform.parent = canvas.transform;
            explosion.transform.parent = null;
                explosion.GetComponent<ParticleSystem>().Play();
                gameObject.layer = 9;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().isTrigger = false;
            StartCoroutine(Force());
            CinemachineCam.instance.noise.m_AmplitudeGain = 10;
            arrow.SetActive(false);

            for (int i = 0; i < tyres.Length; i++)
            {
                tyres[i].transform.parent = null;
                tyres[i].layer = 9;
                if (tyres[i].GetComponent<Rigidbody>() == null)
                {
                    tyres[i].AddComponent<Rigidbody>();
                }
                tyres[i].AddComponent<BoxCollider>();
                tyres[i].GetComponent<Rigidbody>().AddForce(Random.Range(-500, 500), Random.Range(300, 500), Random.Range(-400, 400));
            }

            explosionCollider.SetActive(true);
            transform.tag = "Untagged";
                GetComponent<Tanker>().enabled = false;
            

        }
        
    }
    IEnumerator Force()
    {
        yield return new WaitForSeconds(0.1f);
        
        rb.AddForce(Random.Range(-500, 500), Random.Range(600, 602), Random.Range(-400, 400));
        yield return new WaitForSeconds(0.7f);
        CinemachineCam.instance.noise.m_AmplitudeGain = 0.5f;

    }
}
