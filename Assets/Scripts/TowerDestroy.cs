using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDestroy : MonoBehaviour
{
    public bool destroy;
    int k;
    public  List<GameObject>  pieces;
    public bool MainBool;
    public bool getBigger;
    public float timer;
    public float currentX, currentY, currentZ;
    public float scaleIndex;
    public float maxHealth;
    public float health;
    void Start()
    {
        health = maxHealth;
        for (int i = 0; i < transform.childCount; i++)
        {
            pieces.Add(transform.GetChild(i).gameObject);
        }
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
        
        if (health<=0 && k==0)
        {
            k++;
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<BoxCollider>());
            for (int j = 0; j < transform.childCount; j++)
            {
               // tyres[i].transform.parent = null;
                
                if (pieces[j].GetComponent<Rigidbody>() == null)
                {
                    pieces[j].AddComponent<Rigidbody>();
                }
                pieces[j].AddComponent<BoxCollider>();
                pieces[j].GetComponent<Rigidbody>().AddForce(Random.Range(-400, -200),0,0);
            }

        }
    }
}
