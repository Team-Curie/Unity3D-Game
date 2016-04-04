using UnityEngine;
using System.Collections;

public class shipSpawnScript : MonoBehaviour {

    public GameObject[] shipPrefabs;
    public ShipSelect ship;
	// Use this for initialization
	void Awake () {
        ship = FindObjectOfType<ShipSelect>();
        if (PlayerPrefs.GetInt("shipIndex") != null)
        {
            ship.shipIndex = PlayerPrefs.GetInt("shipIndex");
        }
       
   

        if (ship.shipIndex == 0)
        {
            Instantiate(shipPrefabs[0], this.transform.position, Quaternion.identity);
            Debug.Log("STORM");

        }
        else if (ship.shipIndex == 1)
        {
            Instantiate(shipPrefabs[1], this.transform.position, Quaternion.identity);
            Debug.Log("VIPER");

        }


    }
	
	
}
