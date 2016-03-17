using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    float bulletTimeout = 2;
    float bulletSpeed = 5f;

    void Update()
    {
        transform.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0f, 0f, bulletSpeed), ForceMode.Impulse);

        bulletTimeout -= Time.deltaTime;
        if (bulletTimeout <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}