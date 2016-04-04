/**
 * Controls player based on mouse movement. Static speed value 
 * and no rotation.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    
    public float fuel = 1024f;
    public float shipHealth = 100f;
    public float shipShield = 100f;
    public float mouseSensitivity = 100f;
    public Canvas canvas;
    private Slider[] canvasSliders;
    public Slider fuelSlider;
    public Slider healthSlider;
    public Slider armorShield;
    public bool nearPlanet = false;
    public string nearPlanetName;



    public int maxSpeed = 70;
    public int minSpeed = 10;
    public float rotationSpeed = 150;
    public bool status = false;

    public int currrentSpeed = 30;
    private GameObject[] turbines;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasSliders = canvas.GetComponentsInChildren<Slider>();

        healthSlider = canvasSliders[0];
        fuelSlider = canvasSliders[1];
        armorShield = canvasSliders[2];

        fuelSlider.value = fuel;
        healthSlider.value = shipHealth;
        armorShield.value = shipShield;
    }

    void Update()
    {           

        if (nearPlanet)
        {
            Debug.Log("You are near planet " + nearPlanetName + ": Press E key to land.");
            LandOnPlanet();
        }
    }


    void LateUpdate()
    {
        healthSlider.value = shipHealth;
        armorShield.value = shipShield;
        //Coordinates pause - play with manager object
        if (!status)
        {
            //Rotation manager
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
            else if (Input.GetKey(KeyCode.D))
                transform.Rotate(0, 0, -Time.deltaTime * rotationSpeed);

            //Max speed
            if (Input.GetKey(KeyCode.W))
            {
                currrentSpeed = maxSpeed;
             //   MaxTurbines(0.65f);
            }//Min speed
            else if (Input.GetKey(KeyCode.S))
            {
                currrentSpeed = minSpeed;
              //  MaxTurbines(0.3f);
            }//Cruise speed
            else {
                currrentSpeed = 30;
             //   MaxTurbines(0.55f);
            }

            Vector3 mouseMovement = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2.0f)) * 1;
            transform.Rotate(new Vector3(-mouseMovement.y, mouseMovement.x, -mouseMovement.x) * 0.025f);
            transform.Translate(Vector3.forward * Time.deltaTime * currrentSpeed);
            DecreaseFuel();
        }
    }

    void MaxTurbines(float intensity)
    {
        foreach (GameObject turbine in turbines)
        {
          //  turbine.GetComponent<LensFlare>().brightness = intensity;
        }
    }
    private void DecreaseFuel()
    {
        fuel -= (currrentSpeed / 10);
        fuelSlider.value = fuel;
        //Debug.Log(fuel);
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
