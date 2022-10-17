using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDummy : MonoBehaviour
{
    public float startTime;
    public float alpha;
    private void Update()
    {
        startTime += Time.deltaTime;
        if (startTime > 6f)
        {
            alpha -= 0.01f;
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, alpha);
        }
        if (startTime > 11)
        {
            Destroy(gameObject);
        }
    }
}
