using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRope : MonoBehaviour
{
    public GameObject ropePrefab,ropePrefabConnectedPlayer;
    [HideInInspector]
    public bool Created;
    [HideInInspector]
    public GameObject currentRope;
    [HideInInspector]
    public bool destroyBool;
    GameObject giant;

    private void Start()
    {
        giant = GameObject.FindGameObjectWithTag("GiantParent");
    }
    private void Update()
    { 
        if (!Created && GameControl.instance.totalRopeCount<5)
        {
            if (GetComponent<Line>().endPos.tag == "Giant")
            {
                var newLine = Instantiate(ropePrefabConnectedPlayer, transform.position, Quaternion.identity);
                currentRope = newLine;
                newLine.transform.parent = gameObject.transform;
                GameControl.instance.totalRopeCount++;
                Debug.Log(GameControl.instance.totalRopeCount);
            }
            else
            {
                var newLine = Instantiate(ropePrefab, transform.position, Quaternion.identity);
                currentRope = newLine;
                newLine.transform.parent = gameObject.transform;
                GameControl.instance.totalRopeCount++;
                Debug.Log(GameControl.instance.totalRopeCount);
            }
            
            Created = true;
        }
       
        currentRope.transform.GetChild(1).transform.position =GetComponent<Line>().startPos.position;
        currentRope.transform.GetChild(2).transform.position = GetComponent<Line>().endPos.position;
        
        if (Vector3.Distance(GetComponent<Line>().startPos.position, GetComponent<Line>().endPos.position) < 1)
        {
                Destroy(gameObject);
                Destroy(currentRope);
        }
        if (Vector3.Distance(GetComponent<Line>().startPos.position, GetComponent<Line>().endPos.position) > 20 && GetComponent<Line>().endPos.position.z> GetComponent<Line>().startPos.position.z)
        {
            if (GetComponent<LineFollow>().enabled && !destroyBool)
            {
                StartCoroutine(DestroyRope());
                destroyBool = true;
                
            }
          
        }

        


    }
    IEnumerator DestroyRope()
    {
        yield return new WaitForSeconds(2f);
        GameControl.instance.totalRopeCount--;
        Debug.Log(GameControl.instance.totalRopeCount);
        Destroy(gameObject);
        Destroy(currentRope);
    }
}
