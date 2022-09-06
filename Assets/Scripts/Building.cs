using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    GameObject cam;
    
    
    
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        
    }

    
    void FixedUpdate()
    {
       // Debug.Log(cam.transform.position.z);
        if (cam.transform.position.z > transform.position.z && cam.transform.position.z < transform.position.z + 60 )
        {
            
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {

                transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                   
                
               
            }
            
        }
        
        if(cam.transform.position.z > transform.position.z + 60)
        {
           
            for (int j = 0; j < transform.GetChild(0).childCount; j++)
            {
                transform.GetChild(0).GetChild(j).gameObject.SetActive(false);
            }
          //  GetComponent<Building>().enabled = false;
        }
    }
}
