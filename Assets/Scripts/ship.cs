using UnityEngine;
using System.Collections;

public class ship : MonoBehaviour {

    public int moveSpeed = 100;
    public int mouseSens = 20;
    public bool nearPlanet = false;
    public string nearPlanetName;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        shipController();
        landPlanet();
    }

    void shipController() {
        transform.position += transform.forward * Time.deltaTime * moveSpeed * Input.GetAxis("Vertical");
        //this.GetComponent<Rigidbody>().AddForce(Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed * transform.forward);
        transform.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens, 0);
    }

    void landPlanet() {
        if (nearPlanet == true && Input.GetKeyDown("e")) {
            Application.LoadLevel (nearPlanetName);
        }
    }

    void OnTriggerEnter(Collider target) {
        if (target.gameObject.tag == "planet") {
            nearPlanetName = target.gameObject.name;
        }
    }

    void OnTriggerExit(Collider target) {
        if (target.gameObject.tag == "planet") {
            nearPlanetName = "";
        }
    }


}