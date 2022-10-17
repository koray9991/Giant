using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTanker : MonoBehaviour
{
    
    public float timer, spawnTime;


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime && timer < spawnTime + 0.1f)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

    }

}
