using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameControl : MonoBehaviour
{
    public Camera cam;
    public GameObject linePrefab;
    public GameObject currentLine;
    bool drawing;
    bool laserDrawing;
    [SerializeField] Material laserMat;
    public GameObject laserObject;
    private void FixedUpdate()
    {
        //if (currentLine != null)
        //{
        //    if (currentLine.GetComponent<Line>().endPos != null)
        //    {
        //        Debug.Log(Vector3.Distance(currentLine.GetComponent<Line>().endPos.position, currentLine.GetComponent<Line>().startPos.position));
        //    }

        //}
        if (Input.GetMouseButton(0) && drawing)
        {

            currentLine.GetComponent<LineRenderer>().SetPosition(1, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f)));
            currentLine.GetComponent<LineRenderer>().endWidth = 0.06f;
        }
        if (Input.GetMouseButton(0) && laserDrawing)
        {

            currentLine.GetComponent<LineRenderer>().SetPosition(1, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f)));
            currentLine.GetComponent<LineRenderer>().endWidth = 0.06f;
            
        }
    }
    void Update()
    {
       
            if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);

            
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.tag == "Draw")
                {
                    var newLine = Instantiate(linePrefab, hitInfo.transform.position, Quaternion.identity);           
                    currentLine = newLine;
                    currentLine.GetComponent<LineRenderer>().SetPosition(0, hitInfo.transform.position);
                    currentLine.GetComponent<Line>().startPos = hitInfo.transform;
                    drawing = true;
                  
                }
                if (hitInfo.transform.tag == "LaserBase")
                {
                    var newLine = Instantiate(linePrefab, hitInfo.transform.position, Quaternion.identity);
                    currentLine = newLine;
                    currentLine.GetComponent<LineRenderer>().SetPosition(0, hitInfo.transform.position);
                    currentLine.GetComponent<LineRenderer>().startColor = new Color(1, 0, 0, 0.5f);
                    currentLine.GetComponent<LineRenderer>().endColor = new Color(1,0,0,0.5f);
                    currentLine.GetComponent<LineRenderer>().material = laserMat;
                    currentLine.GetComponent<Line>().startPos = hitInfo.transform;
                    
                    laserDrawing = true;

                }
            }
        }
       
        if (Input.GetMouseButtonUp(0) && drawing)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.tag == "Draw")
                {
                    currentLine.GetComponent<LineRenderer>().SetPosition(1, hitInfo.transform.position);
                    currentLine.GetComponent<LineRenderer>().startWidth = 0f;
                    currentLine.GetComponent<LineRenderer>().endWidth = 0f;
                    currentLine.GetComponent<Line>().endPos = hitInfo.transform;
                    currentLine.GetComponent<CreateRope>().enabled = true;
                  
                }
               else if (hitInfo.transform.tag == "Giant")
                {
                    currentLine.GetComponent<LineRenderer>().SetPosition(1, hitInfo.transform.position);
                    currentLine.GetComponent<LineRenderer>().startWidth = 0f;
                    currentLine.GetComponent<LineRenderer>().endWidth = 0f;
                    currentLine.GetComponent<LineFollow>().enabled = true;
                    currentLine.GetComponent<LineFollow>().endPos = hitInfo.transform;
                    currentLine.GetComponent<Line>().endPos = hitInfo.transform;
                    currentLine.GetComponent<CreateRope>().enabled = true;
                    
                }
                else
                {
                    Destroy(currentLine);
                    
                }

            }
            if (currentLine.GetComponent<Line>().endPos == null)
            {
                Destroy(currentLine);
            }
            drawing = false;
           
        }
        if (Input.GetMouseButtonUp(0) && laserDrawing)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.tag == "LaserBase")
                {
                    currentLine.GetComponent<LineRenderer>().SetPosition(1, hitInfo.transform.position);
                    currentLine.GetComponent<LineRenderer>().startWidth = 0.3f;
                    currentLine.GetComponent<LineRenderer>().endWidth = 0.3f;
                    currentLine.GetComponent<Line>().endPos = hitInfo.transform;
                    var laserObj = Instantiate(laserObject, new Vector3((currentLine.GetComponent<Line>().startPos.position.x + currentLine.GetComponent<Line>().endPos.position.x) / 2, (currentLine.GetComponent<Line>().startPos.position.y + currentLine.GetComponent<Line>().endPos.position.y) / 2, (currentLine.GetComponent<Line>().startPos.position.z + currentLine.GetComponent<Line>().endPos.position.z) / 2), Quaternion.identity);
                   
                    laserObj.transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(currentLine.GetComponent<Line>().endPos.position, currentLine.GetComponent<Line>().startPos.position));
                    laserObj.transform.LookAt(currentLine.GetComponent<Line>().startPos);
                   
                      
                }
                else
                {
                    Destroy(currentLine);

                }
              if(currentLine.GetComponent<Line>().endPos == null)
                {
                    Destroy(currentLine);
                }

               laserDrawing = false;
                
            }
        }
    }
  
}

