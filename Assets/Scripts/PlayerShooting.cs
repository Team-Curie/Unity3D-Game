using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletShootPosition;
    public GameObject bulletObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = GameObject.Instantiate(bulletObject);
            bullet.gameObject.SetActive(true);
            bullet.transform.position = bulletShootPosition.transform.position;
            bullet.transform.rotation = bulletShootPosition.transform.rotation;
            bullet.AddComponent<BulletShooter>();
        }
    }
}
