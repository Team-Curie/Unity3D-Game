using UnityEngine;
using System.Collections;

public class AmmoScript : MonoBehaviour {
    public PlayerShooting player;
    public int ammoAmount;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            player.clipAmmount += ammoAmount;
            Destroy(this.gameObject);
        }
    }


	
}
