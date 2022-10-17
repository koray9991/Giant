using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageText : MonoBehaviour
{
   public float xMove, yMove;
    public float alpha=1;
    RectTransform rt;
    Text txt;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        txt = GetComponent<Text>();
        xMove = Random.Range(-0.05f, 0.05f);
        yMove = Random.Range(0.02f, 0.05f);
        Destroy(gameObject, 2);
    }

   
    void FixedUpdate()
    {
        alpha -= 0.01f;
        rt.localPosition = new Vector3(rt.localPosition.x + xMove, rt.localPosition.y + yMove, rt.localPosition.z);
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, alpha);
    }
}
