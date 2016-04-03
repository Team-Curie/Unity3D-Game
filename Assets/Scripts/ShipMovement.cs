using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    public Transform playerCamera;
    public float fuel = 1024f;
    public float shipHealth = 100f;
    public float shipShield = 100f;
    public float speed = 100f;
    public float mouseSensitivity = 100f;
    public float rotationZ = 0.0f;
    public Slider fuelSlider;
    public Slider healthSlider;
    public Slider armorShield;
    public bool nearPlanet = false;
    public string nearPlanetName;

    void Start()
    {
        fuelSlider.value = fuel;
        healthSlider.value = shipHealth;
        armorShield.value = shipShield;
    }
    void LateUpdate()
    {
        
        healthSlider.value = shipHealth;
        armorShield.value = shipShield;
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

        RotateShip();

        if (nearPlanet)
        {
            Debug.Log("You are near planet " + nearPlanetName + ": Press E key to land.");
            LandOnPlanet();
        }
    }

    void MoveShip()
    {
        this.transform.position += Input.GetAxis("Vertical") * transform.forward * speed * Time.deltaTime;
        this.transform.position += Input.GetAxis("Horizontal") * transform.right * speed * Time.deltaTime;
    }

    private void DecreaseFuel()
    {
        fuel -= (speed / 100);
        fuelSlider.value = fuel;
        //Debug.Log(fuel);
    }

    private void RotateShip()
    {
        this.transform.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime, 0f);
        rotationZ += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationZ = Mathf.Clamp(rotationZ, -50, 50);
        playerCamera.localEulerAngles = new Vector3(-rotationZ, 0, 0);
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
