using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletShootPosition;
    public GameObject bulletObject;
	public int bulletSpeed = 100;
	
    float bulletDamage = 20;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			var bullet = GameObject.Instantiate(bulletObject, bulletShootPosition.transform.position, bulletShootPosition.transform.rotation) as GameObject;
			bullet.GetComponent<Rigidbody> ().AddForce (transform.forward * (bulletSpeed * 10) * Time.deltaTime);
			Destroy (bullet, 2);
        }
    }
}
