using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GunController : MonoBehaviour
{
    private static GunController instance;
    public static GunController Instance { get => instance; set => instance = value; }

    [SerializeField]
    RectTransform cross;
    [SerializeField]
    LayerMask layerMask;
    public GameObject Gun;
    [SerializeField]
    float attackCoolDown;  
    float waitForAttack;   
    Vector2 firstPosition, tempPosition;
    bool onClick;




    public GameObject particle;
    public GameObject animationObject;
    public ParticleSystem GrenadeParticle;
    
    
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
    }

    void Start()
    {
        
        waitForAttack = 0;
        onClick = false;

        firstPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        tempPosition = Vector2.zero;




    }
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }



       
       


        GunMoves();
        if (isMouseOnScreen())
        {
            if (Input.GetMouseButtonDown(0) )
            {
                tempPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                onClick = true;
                //Debug.Log("X: " + firstPosition.x);
                //Debug.Log("Y: " + firstPosition.y);
            }
            else if (Input.GetMouseButtonUp(0) )
            {
                onClick = false;
                firstPosition = cross.position;
            }
            else if (onClick )
            {
                animationObject.transform.Rotate(0, 20, 0);
                SetCrossHair();
                Shoot();
            }
        }
        else
            onClick = false;
    }

    private void SetCrossHair()
    {
        Vector2 mouseDifference = tempPosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 pos = firstPosition - mouseDifference;
        if (pos.x < 0)
            pos.x = 0;
        else if (pos.x > Screen.width)
            pos.x = Screen.width;
        if (pos.y < 0)
            pos.y = 0;
        else if (pos.y > Screen.height)
            pos.y = Screen.height;
        cross.position = pos;
    }

    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(cross.position);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            transform.LookAt(hit.point);
            if (waitForAttack > attackCoolDown)
            {
                waitForAttack = 0;
                GrenadeParticle.Play();
               

               if (hit.transform.gameObject.CompareTag("GiantParent"))
                {
                    Giant.instance.mesh.GetComponent<SkinnedMeshRenderer>().material = Giant.instance.mat[2];
                    StartCoroutine(ColorDefault());
                }
                else
                {
                    Instantiate(particle, hit.point + new Vector3(0, 0.05f, 0), Quaternion.Euler(90, 0, 0));
                }
               

            }
        }
    }
    private void GunMoves()
    {
        waitForAttack += Time.deltaTime;
    }
    bool isMouseOnScreen()
    {
        if (Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height || Input.mousePosition.x < 0 || Input.mousePosition.y < 0)
            return false;
        return true;
    }
    IEnumerator ColorDefault()
    {
        yield return new WaitForSeconds(0.1f);
        Giant.instance.mesh.GetComponent<SkinnedMeshRenderer>().material = Giant.instance.mat[0];
    }
}
