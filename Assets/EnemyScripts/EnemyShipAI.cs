using UnityEngine;
using System.Collections;

public class EnemyShipAI : MonoBehaviour {

    public Transform[] wayPoints;
    public Transform targetPosition;
    public Vector3 moveDirection;
    public Vector3 Velocity;
    public Rigidbody rig;
    public GameObject bullet;
    public bool playerDetected;
    public float range;
    public float attackRange;
    public float speed;
    public int curWayPoint;
    public bool doPatrol = true;
    public float fireRate;
    public float nextFire;
    Vector3 target;
    void Start()
    {
        doPatrol = true;
        targetPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
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
                    Instantiate(bullet, transform.position, transform.rotation); ;
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
    
   
        

 
}
