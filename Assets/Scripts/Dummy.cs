using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject particle;
    public float particleTimerMax;
    float particleTimer;
   public float randomZspeed;
    public int emojiParticleChoose;
    public List<GameObject> emojis;
    
    void Start()
    {
       
       
        randomZspeed = Random.Range(4.2f, 5f);
        particleTimerMax = Random.Range(0f, 20f);
        emojiParticleChoose = Random.Range(0, emojis.Count);
        particle = emojis[emojiParticleChoose];
    }

    
    void FixedUpdate()
    {
       
        if (particleTimer < particleTimerMax && randomZspeed!=0)
        {
            particleTimer += Time.deltaTime;
            if (particleTimer >= particleTimerMax)
            {
                if (particle != null)
                {
                    particle.GetComponent<ParticleSystem>().Play(); 
                    particleTimerMax = Random.Range(0f, 20f);
                }
              
            }
        }
        rb.velocity = new Vector3(0, 0, randomZspeed);

    }
}
