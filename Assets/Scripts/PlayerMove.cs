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
    private Text[] canvasTexts;


    public float maxSpeed = 70f;
    public float minSpeed = 10f;
    public float rotationSpeed = 150f;
    public bool status = false;

    public float currrentSpeed = 30f;
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

        canvasTexts = canvas.GetComponentsInChildren<Text>();
    }

    void Update()
    {

        if (nearPlanet)
        {
            canvasTexts[2].enabled = true;
            //Debug.Log("You are near planet " + nearPlanetName + ": Press E key to land.");
            canvasTexts[2].text = string.Format("You are near planet {0}. Press E key to land.", nearPlanetName);
            LandOnPlanet();
        }
        else
        {
            canvasTexts[2].enabled = false;
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
            else
            {
                currrentSpeed = 30;
                //   MaxTurbines(0.55f);
            }

            Vector3 mouseMovement = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2.0f)) * 0.25f;
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
            //canvasTexts[2].enabled = true;
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.gameObject.tag == "planet")
        {
            nearPlanetName = "";
            //canvasTexts[2].enabled = false;
        }
    }

}
