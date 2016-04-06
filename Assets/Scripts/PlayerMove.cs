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
    public Canvas canvas;
    private Slider[] canvasSliders;
    public Slider fuelSlider;
    public Slider healthSlider;
    public Slider armorShield;
    public bool nearPlanet = false;
    public string nearPlanetName;
    private Text[] canvasTexts;
    public PlayerShooting clips;

    public float maxSpeed = 70f;
    public float minSpeed = 10f;
    public float rotationSpeed = 150f;
    public bool status = false;
    public bool isDestroyed = false;

    public float currrentSpeed = 30f;
    private GameObject[] turbines;
    private int currentMoney;

    void Awake()
    {
        clips = GetComponent<PlayerShooting>();
        clips.clipAmmount = PlayerPrefs.GetInt("clips");
        fuel = PlayerPrefs.GetFloat("shipFuel");
        shipHealth = PlayerPrefs.GetFloat("shipHealth");
        shipShield = PlayerPrefs.GetFloat("shipShield");
        currentMoney = PlayerPrefs.GetInt("Money");

        if (fuel == 0)
        {
            fuel = 1024f;
        }
        if (shipHealth == 0)
        {
            shipHealth = 100f;
        }
        if (shipShield == 0)
        {
            shipShield = 100f;
        }
    }

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasSliders = canvas.GetComponentsInChildren<Slider>();

        // in the Main scene
        // canvasSliders[0] => the FuelSlider in ShipCanvas -> ShipUI -> FuelHolder
        // canvasSliders[1] => the ArmorSlider in ShipCanvas -> ShipUI -> ArmorHolder
        // canvasSliders[2] => the HealthSlider in ShipCanvas -> ShipUI -> HealthHolder
        fuelSlider = canvasSliders[0];
        armorShield = canvasSliders[1];
        healthSlider = canvasSliders[2];

        fuelSlider.value = fuel;
        healthSlider.value = shipHealth;
        armorShield.value = shipShield;

        canvasTexts = canvas.GetComponentsInChildren<Text>();

        // in the Main scene
        // canvasTexts[0] => the RocketCounter from ShipCanvas -> ShipUI -> Ammo
        // canvasTexts[1] => the MoneyCounter from ShipCanvas -> ShipUI -> Currency
        // canvasTexts[2] => the LandingMessage from ShipCanvas
        canvasTexts[1].text = currentMoney.ToString();
    }

    void FixedUpdate()
    {
        if (isDestroyed)
        {
            Debug.Log("YOU ARE DESTROYED. GO HOME.....");
        }
    }

    void Update()
    {
        if (nearPlanet)
        {
            canvasTexts[2].enabled = true;
            Debug.Log("You are near planet " + nearPlanetName + ": Press E key to land.");
            canvasTexts[2].text = string.Format("You are near {0}. Press E key to land.", nearPlanetName);
            LandOnPlanet();
        }

        if (nearPlanetName == "")
        {
            canvasTexts[2].enabled = false;
            nearPlanet = false;
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
        fuel -= (currrentSpeed / 1000);
        fuelSlider.value = fuel;
        //Debug.Log(fuel);
    }

    private void LandOnPlanet()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetFloat("shipFuel", fuel);
            PlayerPrefs.SetFloat("shipShield", shipShield);
            PlayerPrefs.SetFloat("shipHealth", shipHealth);
            PlayerPrefs.SetInt("clips", clips.clipAmmount);
            nearPlanet = false;
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
