using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class Giant : MonoBehaviour
{
    public Rigidbody rb;
    public float startSpeed;
    float velocity;
    public static Giant instance;
  
    bool laserEffect;
    public Animator anim;
    public float maxHealth;
    public float health;
    public GameObject canvas;
    public Image healthBar;
    bool dead;
    //public GameObject Car, carExplosion, camerra, crackScreen;
    //float x, y, z, carExplosionTimer;
    //bool carRot;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        health = maxHealth;
    }
    //IEnumerator CarThrow()
    //{
    //    yield return new WaitForSeconds(1f);

    //    Car.SetActive(true);


    //    yield return new WaitForSeconds(1.5f);

    //    Car.transform.parent = null;
    //  //  Car.AddComponent<Rigidbody>();
    //    //Car.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    //    Car.GetComponent<Rigidbody>().AddForce(0, 100, 900);
    //    x = Random.Range(-5f, 5f);
    //    y = Random.Range(-5f, 5f);
    //    z = Random.Range(-5f, 5f);
    //    carRot = true;


    //    yield return new WaitForSeconds(0.9f);

    //    //Car.SetActive(false);
    //    // carExplosion.transform.position = Car.transform.position;






    //    //Debug.Log("sa");
    //}
    private void FixedUpdate()
    {
        //if (carRot)
        //{
        //    Car.transform.Rotate(x, y, z);
        //    Car.transform.position = Vector3.MoveTowards(Car.transform.position, camerra.transform.position + new Vector3(-0.3f, 0.1f, 3f), 0.8f);
        //    carExplosionTimer += Time.deltaTime;
        //    //if (carExplosionTimer > 0.7f && carExplosionTimer<1.2f)
        //    //{
        //    //    crackScreen.SetActive(true);
        //    //    carExplosion.GetComponent<ParticleSystem>().Play();

        //    //}
        //}
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
        //{
        //    transform.GetComponent<Animator>().SetInteger("movement", 0);


        //}
        //if (transform.name == "SheHulk" && Input.GetKeyDown(KeyCode.T))
        //{
        //    transform.GetComponent<Animator>().SetInteger("movement", 2);
        //    StartCoroutine(CarThrow());
        //    Debug.Log("x");

        //}

        if (GameControl.instance.gunBool)
        {
           
            gameObject.layer = 7;
        }
        if (GameControl.instance.ropeBool)
        {
          
            gameObject.layer = 2;
        }
        float ropeCount = GameObject.FindGameObjectsWithTag("RopeToGiant").Length;
        velocity = startSpeed - ropeCount / 2;
        anim.SetFloat("multiplier", 1-ropeCount/10);
        
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
        if (health <= 0 && !dead)
        {
            dead = true;
            StartCoroutine(GiantDead());
            StartCoroutine(GunController.Instance.KillTheGiant());        }
    }
    IEnumerator GiantDead()
    {
        yield return new WaitForSeconds(1f);
        canvas.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TowerPiece")
        {
            health = 0;
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
        if (other.tag == "TankerExplosion")
        {
            health -= 10;
            healthBar.fillAmount = health / maxHealth;
            Destroy(other.gameObject);
        }
       
        //if (other.tag == "Laser")
        //{
        //    if (transform.name == "Snake")
        //    {


        //       // StartCoroutine(HeadCut());



        //    }
             
        //}
        if (other.tag == "WhiteHouse")
        {
            GameControl.instance.failBool = true;
            GameControl.instance.cinemachineCamGiantVol2.GetComponent<CinemachineVirtualCamera>().Priority = 20;
            //StartCoroutine(GameControl.instance.Fail());
        }

    }


    IEnumerator HeadCut()
    {
        yield return new WaitForSeconds(2.5f);
        if (transform.GetChild(0).name == "Head")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).parent = null;
        }


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
