using UnityEngine;
using System.Collections;

public class EnemyScriptGround : MonoBehaviour {
    // Enemy Stats//
    public float enemyHealth;
    public int armor;
    public int magicResist;
   

   
    public float timer;
    public float patrolSpeed;
    public float chaseSpeed;
    public float chaseWaitTime;
    public float patrolWaitTime;
    public Transform[] patrolWayPoints;
    public float distance;
    public int destPoint;
    public bool isCaster;
    public float sightRange;
    public float attackRange;
    public Transform target;
    public GameObject[] itemLoot;
    private NavMeshAgent agent;
    public Animation anim;

    
        

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyHealth <= 100)
        {
          

               
            


            distance = Vector3.Distance(target.position, transform.position);
            if (distance < sightRange)
            {
                
                agent.SetDestination(target.position);
                transform.LookAt(target);
                agent.speed = 15;
                agent.acceleration = 50;
                if (distance < attackRange)
                {
                    
                    
                    this.GetComponent<Animation>().Play("Attack_01");
                    Debug.Log("hit");
                }

            }
            else
                GotoNextPoint();
        }
        if(enemyHealth <= 0)
        {

           

            Instantiate(itemLoot[Random.Range(0, itemLoot.Length)], transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }


    }

     void GotoNextPoint()
    {
        this.GetComponent<Animation>().Play("Walk");

        // Returns if no points have been set up
        if (patrolWayPoints.Length == 0)
        {
            return;
        }

        else if(agent.remainingDistance < 6f){
            agent.speed = 10f;
            destPoint = Random.Range(0, patrolWayPoints.Length);  // random patrol route
            agent.destination = patrolWayPoints[destPoint].position;
           // not random //destPoint = (destPoint + 1) % patrolWayPoints.Length;
          
        }
   }
    
   public void takeDamage(int dmg)
    {
        enemyHealth -= dmg;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "bullet")
        {
            Debug.Log("AARGH");
            takeDamage(20);
        }
    }


}
