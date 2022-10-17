using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLights : MonoBehaviour
{
    public GameObject red, blue;
    public float timer;
    public float changeColorTime;
    private void Start()
    {
        changeColorTime = Random.Range(0.2f, 0.5f);
    }

    private void Update()
    {
        if (GameControl.instance.tutorial)
        {
            timer += Time.deltaTime;

            if (timer < changeColorTime)
            {
                red.SetActive(true);
                blue.SetActive(false);
            }
            if (timer > changeColorTime && timer < changeColorTime * 2)
            {
                red.SetActive(false);
                blue.SetActive(true);
            }
            if (timer > changeColorTime * 2)
            {
                timer = 0;
            }
        }
        else
        {
            Destroy(gameObject);
        }
      
    }
}
