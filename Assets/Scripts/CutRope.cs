using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;


public class CutRope : MonoBehaviour
{
    ObiRope rope;
    public bool Cut;
    GameObject giant;
    bool destroyed;
    [SerializeField] 
    float ropeDestroyDistance;
    private void Start()
    {
        rope = GetComponent<ObiRope>();
        giant = GameObject.FindGameObjectWithTag("GiantParent");

    }
    private void Update()
    {
  
        if(giant.transform.position.z>(transform.parent.root.GetComponent<Line>().startPos.position.z+ transform.parent.root.GetComponent<Line>().endPos.position.z) / 2+ropeDestroyDistance)
        {
            Cut = true;
        }
       
        if (Cut && !destroyed)
        {
            rope.Tear(rope.elements[rope.elements.Capacity / 2]);
            rope.RebuildConstraintsFromElements();
            destroyed = true;
            GameControl.instance.totalRopeCount--;
            Debug.Log(GameControl.instance.totalRopeCount);
            StartCoroutine(DestroyRope());
        }
        
    }
    IEnumerator DestroyRope()
    {
        yield return new WaitForSeconds(10f);
        Destroy(transform.parent.root.gameObject);
    }
    
}
