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
       
        if (cam.transform.position.z > transform.position.z && cam.transform.position.z < transform.position.z + 70 )
        {
            
            for (int i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                
                   
                
               
            }
            
        }
        
        if(cam.transform.position.z > transform.position.z + 70)
        {
           
            for (int j = 0; j < transform.childCount; j++)
            {
                if (transform.GetChild(j).gameObject.activeSelf)
                {
                    transform.GetChild(j).gameObject.SetActive(false);
                }
                    
            }
        
        }
    }
}
