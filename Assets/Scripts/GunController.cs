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
    public GameObject wereWolfBlode,MinecraftBlode,MommyBlode,snakeBlode,skeletonBlode,cactusBlode,golemBlode;
    public GameObject animationObject;
    public ParticleSystem GrenadeParticle;
    public GameObject textDamage;
    public GameControl gm;
    public GameObject laserRed;
    public GameObject headShot;
    //  public LineRenderer lr;
   // public GameObject spawnPoint;
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
    private void FixedUpdate()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

     //   lr.SetPosition(0, GrenadeParticle.transform.position);





        GunMoves();
        if (isMouseOnScreen())
        {
            if (Input.GetMouseButtonDown(0) )
            {
                tempPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                onClick = true;
            
            }
            else if (Input.GetMouseButtonUp(0) )
            {
                onClick = false;
                firstPosition = cross.position;
              //  lr.gameObject.SetActive(false);
            }
            else if (onClick && GameControl.instance.gunBool )
            {
                animationObject.transform.Rotate(0, 20, 0);
                SetCrossHair();
                Shoot();
              //  lr.gameObject.SetActive(true);
            }
            else
            {
                GetComponent<Animator>().SetFloat("multiplier", 0.0001f);
                cross.GetComponent<Animator>().enabled = false;
                cross.localScale = new Vector3(1.75f, 1.75f, 1);
              //  lr.gameObject.SetActive(false);
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
            
            if (waitForAttack > attackCoolDown && !gm.winBool && !gm.failBool)
            {
                waitForAttack = 0;
                //for (int i = 0; i < GrenadeParticle.transform.childCount; i++)
                //{
                //    GrenadeParticle.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
                //}
                GrenadeParticle.Play();
                GetComponent<Animator>().SetFloat("multiplier", 1f);

                var laser = Instantiate(laserRed, GrenadeParticle.transform.position, Quaternion.identity);//Euler(transform.eulerAngles.x, transform.eulerAngles.y,0));
                laser.GetComponent<LaserMove>().hitPoint = hit.point;
              //  laser.transform.LookAt(transform);
                //if (hit.collider)
                //{
                //    lr.GetComponent<LineRenderer>().SetPosition(1, hit.point);
                //}
                //else
                //{
                //    lr.GetComponent<LineRenderer>().SetPosition(1, transform.forward*500);
                //}
                if (hit.transform.gameObject.CompareTag("GiantParent"))
                {
                    if (hit.transform.gameObject.name == "Golem")
                    {
                        Instantiate(golemBlode, hit.point, Quaternion.identity);

                        if (hit.point.y > 10)
                        {
                            var newHeadShot = Instantiate(headShot, hit.point + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newHeadShot.transform.parent = Giant.instance.canvas.transform;
                        }
                        else
                        {
                            var newTextDamage = Instantiate(textDamage, hit.point, Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newTextDamage.transform.parent = Giant.instance.canvas.transform;
                        }
                    }
                    if (hit.transform.gameObject.name == "Cactus")
                    {
                        Instantiate(cactusBlode, hit.point, Quaternion.identity);
                       
                        if (hit.point.y > 10)
                        {
                            var newHeadShot = Instantiate(headShot, hit.point + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newHeadShot.transform.parent = Giant.instance.canvas.transform;
                        }
                        else
                        {
                            var newTextDamage = Instantiate(textDamage, hit.point, Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newTextDamage.transform.parent = Giant.instance.canvas.transform;
                        }
                    }
                    if (hit.transform.gameObject.name == "Werewolf")
                    {
                        Instantiate(wereWolfBlode, hit.point, Quaternion.identity);
                        
                        if (hit.point.y > 10)
                        {
                            var newHeadShot = Instantiate(headShot, hit.point + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newHeadShot.transform.parent = Giant.instance.canvas.transform;
                        }
                        else
                        {
                            var newTextDamage = Instantiate(textDamage, hit.point, Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newTextDamage.transform.parent = Giant.instance.canvas.transform;
                        }
                    }
                    if (hit.transform.gameObject.name == "Mommy")
                    {
                        Instantiate(MommyBlode, hit.point, Quaternion.identity);
                        if (hit.point.y > 17)
                        {
                            var newHeadShot = Instantiate(headShot, hit.point + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newHeadShot.transform.parent = Giant.instance.canvas.transform;
                        }
                        else
                        {
                            var newTextDamage = Instantiate(textDamage, hit.point, Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newTextDamage.transform.parent = Giant.instance.canvas.transform;
                        }
                    }
                    if (hit.transform.gameObject.name == "Minecraft")
                    {
                        Instantiate(MinecraftBlode, hit.point, Quaternion.identity);
                        if (hit.point.y > 17)
                        {
                            var newHeadShot = Instantiate(headShot, hit.point + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newHeadShot.transform.parent = Giant.instance.canvas.transform;
                        }
                        else
                        {
                            var newTextDamage = Instantiate(textDamage, hit.point, Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newTextDamage.transform.parent = Giant.instance.canvas.transform;
                        }
                    }
                    if (hit.transform.gameObject.name == "Snake")
                    {
                        Instantiate(snakeBlode, hit.point, Quaternion.identity);
                        if (hit.point.y > 13)
                        {
                            var newHeadShot = Instantiate(headShot, hit.point + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newHeadShot.transform.parent = Giant.instance.canvas.transform;
                        }
                        else
                        {
                            var newTextDamage = Instantiate(textDamage, hit.point, Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newTextDamage.transform.parent = Giant.instance.canvas.transform;
                        }
                    }
                    if (hit.transform.gameObject.name == "Skeleton")
                    {
                        Instantiate(skeletonBlode, hit.point, Quaternion.identity);
                        if (hit.point.y > 15)
                        {
                            var newHeadShot = Instantiate(headShot, hit.point + new Vector3(0, 2, 0), Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newHeadShot.transform.parent = Giant.instance.canvas.transform;
                        }
                        else
                        {
                            var newTextDamage = Instantiate(textDamage, hit.point, Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
                            newTextDamage.transform.parent = Giant.instance.canvas.transform;
                        }
                    }
                   
                    cross.GetComponent<Image>().color = Color.red;
                    Giant.instance.health -= 1;
                    Giant.instance.healthBar.fillAmount = Giant.instance.health / Giant.instance.maxHealth;
                    cross.GetComponent<Animator>().enabled = true;
                  //  Debug.Log(hit.point.y);
                    
                    // Giant.instance.mesh.GetComponent<SkinnedMeshRenderer>().material = Giant.instance.mat[2];
                    //StartCoroutine(ColorDefault());
                }
                else if (hit.transform.gameObject.CompareTag("GasStation"))
                {
                    cross.GetComponent<Image>().color = Color.red;
                    hit.transform.GetComponent<GasStation>().MainBool = true;
                    hit.transform.GetComponent<GasStation>().health -= 1;
                    cross.GetComponent<Animator>().enabled = true;
                    Instantiate(particle, hit.point + new Vector3(0, 0.05f, 0), Quaternion.Euler(90, 0, 0));
                    
                }
                else if (hit.transform.gameObject.CompareTag("Tanker"))
                {
                    cross.GetComponent<Image>().color = Color.red;
                    hit.transform.GetComponent<Tanker>().MainBool = true;
                    hit.transform.GetComponent<Tanker>().health -= 1;
                    cross.GetComponent<Animator>().enabled = true;
                   
                }
                else if (hit.transform.gameObject.CompareTag("Car"))
                {
                    cross.GetComponent<Image>().color = Color.red;
                    hit.transform.GetComponent<Car>().MainBool = true;
                    hit.transform.GetComponent<Car>().health -= 1;
                    cross.GetComponent<Animator>().enabled = true;
                }
                else if (hit.transform.gameObject.CompareTag("Human"))
                {
                    cross.GetComponent<Image>().color = Color.red;
                    hit.transform.GetComponent<Animator>().SetInteger("movement", 1);
                    hit.transform.GetComponent<Dummy>().randomZspeed = 0;
                    cross.GetComponent<Animator>().enabled = true;
                }
                else if(hit.transform.gameObject.CompareTag("Tower"))
                {
                    cross.GetComponent<Image>().color = Color.red;
                    // hit.transform.GetComponent<TowerDestroy>().destroy =true;
                    hit.transform.GetComponent<TowerDestroy>().MainBool = true;
                    hit.transform.GetComponent<TowerDestroy>().health -= 1;
                    if (hit.transform.GetComponent<TowerDestroy>().health <= 0)
                    {
                        GameObject.FindGameObjectWithTag("ArrowTower").SetActive(false);

                    //    StartCoroutine(KillTheGiant());

                    }

                }
                else
                {
                    cross.GetComponent<Image>().color = Color.green;
                    cross.GetComponent<Animator>().enabled = false;
                    Instantiate(particle, hit.point + new Vector3(0, 0.05f, 0), Quaternion.Euler(90, 0, 0));
                }
               

            }
        }
    }
   public IEnumerator KillTheGiant()
    {
        var giant = GameObject.FindGameObjectWithTag("GiantParent");
       
            giant.GetComponent<Animator>().SetInteger("movement", 1);
            giant.GetComponent<Rigidbody>().isKinematic = true;
            giant.GetComponent<Giant>().health = 0;
            giant.GetComponent<Giant>().healthBar.fillAmount = Giant.instance.health / Giant.instance.maxHealth;
        
        yield return new WaitForSeconds(2f);

        gm.winPanel.SetActive(true);
        cross.GetComponent<Image>().enabled = false;
        

        
        
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
        //Giant.instance.mesh.GetComponent<SkinnedMeshRenderer>().material = Giant.instance.mat[0];
    }
}
