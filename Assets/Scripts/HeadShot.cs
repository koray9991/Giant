using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeadShot : MonoBehaviour
{
    public float xMove, yMove;
    public float alpha = 0.5f;
    Transform rt;
    SpriteRenderer txt;
    void Start()
    {
        rt = GetComponent<Transform>();
        txt = GetComponent<SpriteRenderer>();
        xMove = Random.Range(-0.05f, 0.05f);
        yMove = Random.Range(0.04f, 0.06f);
        Destroy(gameObject, 2);
    }


    void FixedUpdate()
    {
        alpha -= 0.02f;
        rt.localPosition = new Vector3(rt.localPosition.x + xMove, rt.localPosition.y + yMove, rt.localPosition.z);
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, alpha);
    }
}
