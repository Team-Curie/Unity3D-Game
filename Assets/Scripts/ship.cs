using UnityEngine;
using System.Collections;

public class ship : MonoBehaviour
{
    public bool nearShip = false;
    public string nearPlanetName;
    private PlayerMovement2 player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2>();
    }

    // Update is called once per frame
    void Update()
    {
        landPlanet();
    }

    void landPlanet()
    {
        if (nearShip == true && Input.GetKeyDown("e"))
        {
            PlayerPrefs.SetInt("Money", player.currency);
            PlayerPrefs.SetInt("clips", player.clip.clipAmmount);
            //Debug.Log(PlayerPrefs.GetInt("Money"));
            Application.LoadLevel(nearPlanetName);
        }
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            nearShip = true;
            player.escapeMessage.text = "Press the E key to escape from the planet.";
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            nearShip = false;
            player.escapeMessage.text = "";
        }
    }
}