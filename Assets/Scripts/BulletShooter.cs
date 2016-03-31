﻿using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    float bulletTimeout = 20;
    float bulletSpeed = 0.05f;
    float bulletDamage = 50;

    void Update()
    {
        transform.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0f, 0f, bulletSpeed), ForceMode.Impulse);
        //transform.position += transform.forward * Time.deltaTime * bulletSpeed;

        bulletTimeout -= Time.deltaTime;
        if (bulletTimeout <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}