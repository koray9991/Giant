using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollow : MonoBehaviour
{
    public Transform endPos;
 
    void Update()
    {
        GetComponent<LineRenderer>().SetPosition(1, endPos.position);
    }
}
