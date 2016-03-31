using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletShootPosition;
    public GameObject bulletObject;

    GameObject bullet;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			bullet = GameObject.Instantiate(bulletObject, bulletShootPosition.transform.position, bulletShootPosition.transform.rotation) as GameObject;
            bullet.gameObject.SetActive(true);
            bullet.AddComponent<BulletShooter>();
        }
    }
}
