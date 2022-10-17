using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStation : MonoBehaviour
{
    public static GasStation instance;
    public bool MainBool;
    public bool getBigger;
    public float timer;

    public float currentX, currentY, currentZ;
    public float scaleIndex;

    public float maxHealth;
    public float health;

    public GameObject explosion, explosionCollider;
    public GameObject fire;
    public Material blaclMat;
    public GameObject arrow;

   
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        health = maxHealth;
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
            arrow.SetActive(false);
                explosion.GetComponent<ParticleSystem>().Play();
                GetComponent<MeshRenderer>().material = blaclMat;
                fire.SetActive(true);
                GetComponent<BoxCollider>().enabled = false;
            explosionCollider.SetActive(true);
            StartCoroutine(Force());
            CinemachineCam.instance.noise.m_AmplitudeGain = 10;
            GetComponent<GasStation>().enabled = false;
            
        }
    }
    IEnumerator Force()
    {
        
        yield return new WaitForSeconds(0.7f);
        CinemachineCam.instance.noise.m_AmplitudeGain = 0.5f;

    }
}
