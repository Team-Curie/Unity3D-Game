using UnityEngine;
using System.Collections;

public class BulletParticleDestroy : MonoBehaviour {



    void Update()
    {
        Destroy(this.gameObject, 1f);
    }
}
