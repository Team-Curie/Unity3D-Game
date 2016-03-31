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
            bullet = GameObject.Instantiate(bulletObject);
            bullet.gameObject.SetActive(true);
            bullet.transform.position = bulletShootPosition.transform.position;
            bullet.transform.rotation = bulletShootPosition.transform.rotation;
            bullet.AddComponent<BulletShooter>();
        }
    }
}
