using UnityEngine;
using System.Collections;

public class EnemyShipAI : MonoBehaviour {
    public float enemyHealth;
    public Transform[] wayPoints;
    public Transform targetPosition;
    public Vector3 moveDirection;
    public Vector3 Velocity;
    public Rigidbody rig;
    public GameObject bullet;
    public GameObject[] itemLoot;
    public ParticleSystem laser;
    public PlayerMove playerShip;
    public bool playerDetected;
    public float range;
    public float attackRange;
    public float speed;
    public int curWayPoint;
    public bool doPatrol = true;
    public int enemyShipDamage;
    public float fireRate;
    public float nextFire;
    Vector3 target;

    void Start()
    {
        doPatrol = true;
        targetPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyHealth <= 100)
        {



            if (doPatrol)
            {

                if (curWayPoint < wayPoints.Length)
                {
                    target = wayPoints[curWayPoint].position;
                    moveDirection = target - transform.position;
                    Velocity = rig.velocity;

                    if (moveDirection.magnitude < 1)
                    {
                        curWayPoint++;
                    }
                    else
                    {
                        Velocity = moveDirection.normalized * speed;
                    }
                }
                else
                {
                    curWayPoint = 0;
                }
                if (Vector3.Distance(transform.position, targetPosition.position) < range)
                {
                    Debug.Log("inRange");
                    doPatrol = false;
                }

                rig.velocity = Velocity;
                transform.LookAt(target);

            }



            if (!doPatrol)
            {

                target = targetPosition.position;
                moveDirection = target - transform.position;
                Velocity = moveDirection.normalized * speed;
                transform.LookAt(targetPosition);


                if (Vector3.Distance(transform.position, targetPosition.position) <= attackRange && !doPatrol)
                {

                    target = targetPosition.position;
                    Velocity = moveDirection.normalized * 0;
                    transform.LookAt(targetPosition);
                    if (Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        laser.Play();
                        if (playerShip.shipShield > 0)
                        {
                            playerShip.shipShield -= enemyShipDamage;
                           
                        }
                        else if(playerShip.shipShield <= 0)
                        {
                            playerShip.shipHealth -= enemyShipDamage;
                        }
                    }

                }
                if (Vector3.Distance(transform.position, targetPosition.position) > range)
                {
                    doPatrol = true;
                }



                rig.velocity = Velocity;
                transform.LookAt(target);



            }


        }
        if(enemyHealth <= 0)
        {
            enemyHealth = 0;


            Instantiate(itemLoot[Random.Range(0, itemLoot.Length)], transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }

    public void takeDamage(int dmg)
    {
        enemyHealth -= dmg;
    }



}
