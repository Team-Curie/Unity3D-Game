using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ShipMovement : MonoBehaviour
{
    public float fuel = 1024f;
    public float speed = 10f;
    public float mouseSensitivity = 100f;

    public bool nearPlanet = false;
    public string nearPlanetName;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetAxis("Vertical") != 0)
        {
            //Debug.Log("Moving");
            MoveShip();
            DecreaseFuel();
        }

        if (nearPlanet)
        {
            Debug.Log("You are near planet " + nearPlanetName + ": Press E key to land.");
            LandOnPlanet();
        }
    }

    void MoveShip()
    {
        //this.transform.Translate(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), (this.transform.position.z + speed) * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * speed * Input.GetAxis("Vertical");
        //transform.Rotate(0f, Input.GetAxis("Mouse X") * turnRate, 0f);
        //transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0f));
        transform.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);
    }

    private void DecreaseFuel()
    {
        fuel -= (speed / 1000);
        Debug.Log(fuel);
    }

    private void LandOnPlanet()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nearPlanetName);
        }
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "planet")
        {
            nearPlanetName = target.gameObject.name;
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.gameObject.tag == "planet")
        {
            nearPlanetName = "";
        }
    }
}
