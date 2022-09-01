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
  
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && drawing)
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
                    currentLine.GetComponent<LineRenderer>().startWidth = 0.2f;
                    currentLine.GetComponent<LineRenderer>().endWidth = 0.2f;
                    currentLine.GetComponent<Line>().startPos = hitInfo.transform;
                    drawing = true;
                  
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
            drawing = false;
            currentLine = null;
        }
    }
  
}

