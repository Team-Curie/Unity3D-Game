using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    float bulletTimeout = 20;
    float bulletSpeed = 1f;
    float bulletDamage = 50;

    void Update()
    {
        this.transform.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0f, 0f, bulletSpeed), ForceMode.Impulse);
        //transform.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        //transform.position += transform.forward * Time.deltaTime * bulletSpeed;

        bulletTimeout -= Time.deltaTime;
        if (bulletTimeout <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}