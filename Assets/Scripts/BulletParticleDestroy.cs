using UnityEngine;
using System.Collections;

public class BulletParticleDestroy : MonoBehaviour {



    void Update()
    {
        Destroy(this.gameObject, 0.3f);
    }
}
