using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{

    Camera camera;
    EnemyScriptGround enemy;
    EnemyShipAI enemyShip;
    public GameObject bulletShootPosition;
    //  public GameObject bulletObject;
    public AudioSource[] shot;
    public Transform[] effect;
    public ParticleSystem muzzle;
    public Light light;
    public Text bulletDisplay;
    public float playerShootRange;
    public int bulletSpeed = 100;
    public int bulletDamage = 50;
    public int currentBulletAmount;
    private int defaultBulletCount = 30;
    public int clipAmmount;
    public bool reload;
    public float reloadTimer = 3f;
    public PauseControllerScript pauseMenu;


    void Start()
    {
        pauseMenu = GameObject.FindObjectOfType<PauseControllerScript>();
        bulletDisplay = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponentInChildren<Text>();
        reload = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        reloadTimeOutCheck();
    }

    void Update()
    {
        bulletDisplay.text = currentBulletAmount.ToString() + "/" + clipAmmount.ToString();
        if (clipAmmount < 0)
        {
            clipAmmount = 0;
        }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));

        if (!pauseMenu.isPaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentBulletAmount > 0 && reload && clipAmmount >= 0)
                {
                    muzzle.Play();

                    shot[0].Play();
                    currentBulletAmount -= 1;
                    if (Physics.Raycast(ray, out hit, playerShootRange))
                    {
                        //Debug.Log("Shooooooot");
                        //currentBulletAmount -= 1;
                        //if(hit.collider == null)
                        //{
                        //    currentBulletAmount -= 1;
                        //}

                        if (hit.collider.gameObject.tag != "Enemy" && hit.collider.gameObject.tag != "EnemyShip")
                        {
                            var particleClone = Instantiate(effect[0], hit.point, Quaternion.LookRotation(hit.normal));
                        }
                        else if (hit.collider.gameObject.tag == "Enemy")
                        {
                            EnemyScriptGround enemy = hit.collider.GetComponent<EnemyScriptGround>();
                            enemy.takeDamage(bulletDamage);
                            var particleClone = Instantiate(effect[1], hit.point, Quaternion.LookRotation(hit.normal));
                        }
                        else if (hit.collider.gameObject.tag == "EnemyShip")
                        {
                            Debug.Log("DAMAGE TAKEN");
                            EnemyShipAI enemyShip = hit.collider.GetComponent<EnemyShipAI>();
                            enemyShip.takeDamage(bulletDamage);
                            var particleClone = Instantiate(effect[0], hit.point, Quaternion.LookRotation(hit.normal));
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if ((currentBulletAmount <= 0 || currentBulletAmount != defaultBulletCount) && clipAmmount > 0)
                {
                    shot[1].Play();
                    clipAmmount -= 1;
                    currentBulletAmount = defaultBulletCount;
                    reload = false;
                }
            }
        }

    }
    private void reloadTimeOutCheck()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer <= 0)
        {
            reload = true;
            reloadTimer = 3f;
        }
    }







}
