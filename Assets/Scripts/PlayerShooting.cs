using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletShootPosition;
    public GameObject bulletObject;
    
    public Text bulletDisplay;
	public int bulletSpeed = 100;
	float bulletDamage = 50;
    public int currentBulletAmount;
    private int defaultBulletCount = 30;
    public int clipAmmount;
    public bool reload;
    public float reloadTimer = 3f;
    void Start()
    {
        reload = true;
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        reloadTimeOutCheck();
    }

    void Update()
    {
        if(clipAmmount < 0)
        {
            clipAmmount = 0;
        }
        bulletDisplay.text = currentBulletAmount.ToString() + "/" + clipAmmount.ToString(); ;
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBulletAmount > 0 && reload && clipAmmount >= 0 )
            {

                var bullet = GameObject.Instantiate(bulletObject, bulletShootPosition.transform.position, bulletShootPosition.transform.rotation) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * (bulletSpeed * 10) * Time.deltaTime);
                currentBulletAmount -= 1;
                Destroy(bullet, 2);

            }
           

        }
        if (Input.GetMouseButtonDown(1))
           
        {
                                                            
            
            if (currentBulletAmount <= 0 || currentBulletAmount != defaultBulletCount && clipAmmount >0)
            {
                clipAmmount -= 1;
                currentBulletAmount = defaultBulletCount;
                reload = false;

            }

        }

    }
    private void  reloadTimeOutCheck()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer <= 0)
        {
            reload = true;
            reloadTimer = 3f;
        }
    }

    




}
