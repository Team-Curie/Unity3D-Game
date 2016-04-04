using UnityEngine;
using System.Collections;

public class ship : MonoBehaviour {

    public bool nearShip = false;
    public string nearPlanetName;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
      
        landPlanet();
    }

   

    void landPlanet() {
        if (nearShip == true && Input.GetKeyDown("e")) {
            Application.LoadLevel (nearPlanetName);
        }
    }

    void OnTriggerEnter(Collider target) {
        if (target.gameObject.tag == "Player") {
            nearShip = true;
        }
    }

    void OnTriggerExit(Collider target) {
        if (target.gameObject.tag == "Player") {
            nearShip = false;
        }
    }


}