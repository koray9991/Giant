using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public Camera cam;
    public GameObject linePrefab;
    public GameObject currentLine;
    bool drawing;
    bool laserDrawing;
    [SerializeField] Material laserMat;
    public GameObject laserObject;

    public bool gunBool, ropeBool;
    public GameObject gunObject;
    float timer;
    public GameObject virtualCam;
    public GameObject virtualCamWhiteHouse;
    public int totalRopeCount;
    public bool tutorial;
    public GameObject winPanel;
    public GameObject failPanel;
    public bool winBool;
    public bool failBool;
    public GameObject cinemachineCamGiantVol2;
    public float ropeTimer, gunTimer;
    float handTimer;
    public GameObject hand1, hand2;
    float UiTimer;
    public GameObject TieTheGiantText, ShootTheGiantText;
    public TextMeshProUGUI levelText;
    float textAlpha;
    float alphaTimer;
   public int level;
    public GameObject level1Lights;
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        tutorial = true;
        Time.timeScale = 1;
        textAlpha = 0;
        level =  PlayerPrefs.GetInt("level");
        
        PlayerPrefs.SetInt("level", level);
        levelText.text = "LEVEL " + (level+1).ToString();
        StartCoroutine(TutorialEnd());
    }
    IEnumerator TutorialEnd()
    {
        yield return new WaitForSeconds(3.5f);
        tutorial = false;
        virtualCamWhiteHouse.GetComponent<CinemachineVirtualCamera>().Priority = 0;
        gunBool = true;
        ropeBool = false;
    }
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(ropeBool && !winBool && !failBool)
            {
                handTimer += Time.deltaTime;
                if (handTimer < 2)
                {
                    hand1.SetActive(true);
                    hand2.SetActive(false);
                }
                if (handTimer > 2 && handTimer<4)
                {
                    hand1.SetActive(false);
                    hand2.SetActive(true);
                }
                if (handTimer > 4)
                {
                    handTimer = 0;

                }
            }
            else
            {
                hand1.SetActive(false);
                hand2.SetActive(false);
            }
        }
        if (alphaTimer < 6)
        {
            levelText.color = new Color(levelText.color.r, levelText.color.g, levelText.color.b, textAlpha);
            alphaTimer += Time.deltaTime;
            if (alphaTimer < 3)
            {
                textAlpha += 0.01f;
            }
            if (alphaTimer > 3)
            {
                textAlpha -= 0.01f;
            }
        }
        else
        {
            Destroy(levelText);
        }
        if (ropeBool && currentLine!=null && !winBool && !failBool && totalRopeCount<5)
        {
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
        if (failBool)
        {
            gunBool = false;
            ropeBool = false;
            gunObject.SetActive(false);
            if (Time.timeScale > 0)
            {
                Time.timeScale -= 0.01f;
                if (Time.timeScale < 0.5F)
                {
                    failPanel.SetActive(true);
                }
            }
            else
            {
                Time.timeScale = 0;
              
            }
        }
    }
    IEnumerator UiDisable()
    {
        yield return new WaitForSeconds(3f);
        TieTheGiantText.SetActive(false);
        ShootTheGiantText.SetActive(false);
    }
    void Update()
    {
        if (tutorial == false && !winBool && !failBool)
        {
            timer += Time.deltaTime;
        }
       

        if (timer > gunTimer && gunBool && !tutorial && !winBool && !failBool)
        {
                gunBool = false;
                ropeBool = true;
            
           
            timer = 0;
        }
        if (timer > ropeTimer && ropeBool && !tutorial && !winBool && !failBool)
        {
            ropeBool = false;
            gunBool = true;


            timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);

            
        }

        
        if (ropeBool && !winBool && !failBool && totalRopeCount < 5)
        {
            if (gunObject.activeSelf)
            {
                gunObject.SetActive(false);
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    TieTheGiantText.SetActive(true);
                    StartCoroutine(UiDisable());
                    level1Lights.SetActive(true);
                }
                   
                
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
                        currentLine.GetComponent<LineRenderer>().endColor = new Color(1, 0, 0, 0.5f);
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
                
                drawing = false;
                currentLine = null;
            }
            if (Input.GetMouseButtonUp(0) && laserDrawing)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.transform.tag == "LaserBase")
                    {
                        currentLine.GetComponent<LineRenderer>().SetPosition(1, hitInfo.transform.position);
                        currentLine.GetComponent<LineRenderer>().startWidth = 0f;
                        currentLine.GetComponent<LineRenderer>().endWidth = 0f;
                        currentLine.GetComponent<Line>().endPos = hitInfo.transform;
                        var laserObj = Instantiate(laserObject, new Vector3((currentLine.GetComponent<Line>().startPos.position.x + currentLine.GetComponent<Line>().endPos.position.x) / 2, (currentLine.GetComponent<Line>().startPos.position.y + currentLine.GetComponent<Line>().endPos.position.y) / 2, (currentLine.GetComponent<Line>().startPos.position.z + currentLine.GetComponent<Line>().endPos.position.z) / 2), Quaternion.identity);

                        laserObj.transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(currentLine.GetComponent<Line>().endPos.position, currentLine.GetComponent<Line>().startPos.position));
                        laserObj.transform.LookAt(currentLine.GetComponent<Line>().startPos);


                    }
                    else
                    {
                        Destroy(currentLine);

                    }
                   
                    currentLine = null;
                    laserDrawing = false;

                }
            }
        }
        if (gunBool && !winBool && !failBool)
        {
            if (currentLine != null)
            {
                Destroy(currentLine);
            }
            if (!gunObject.activeSelf)
            {
                gunObject.SetActive(true);
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    ShootTheGiantText.SetActive(true);
                    StartCoroutine(UiDisable());
                    level1Lights.SetActive(false);
                }
                    
            }

        }



    }
 //public IEnumerator Fail()
 //   {

       
     
 //   }
}

