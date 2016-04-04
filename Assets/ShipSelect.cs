using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShipSelect : MonoBehaviour {
    public GameObject[] ships;
    public Text shipText;
    public int shipIndex;
    



	// Use this for initialization
	void Start () {
        


        if (shipIndex != null)
        {
            shipIndex = PlayerPrefs.GetInt("shipIndex",0);
        }
	}
	
	// Update is called once per frame
	void Update () {



        if (shipIndex == 1 && ships[0] != null)
        {
            ships[0].SetActive(false);
            ships[1].SetActive(true);
            shipText.text = ships[1].name;
        }
        if (shipIndex == 0 && ships[1] != null)
        {
            ships[0].SetActive(true);
            ships[1].SetActive(false);
            shipText.text = ships[0].name;
        }
        DontDestroyOnLoad(this.gameObject);

    }

    public void loadToMainScene()
    {
        Application.LoadLevel("main");
    }


    public void selectShipOne()
    {
        shipIndex++;
       
        if (shipIndex == ships.Length)
        {

            shipIndex = 0;
            PlayerPrefs.SetInt("shipIndex", shipIndex);
        }
       
    }
    public void selectShipTwo()
    {
        shipIndex--;
        if (shipIndex < 0)
        {
            shipIndex = ships.Length -1;
            PlayerPrefs.SetInt("shipIndex", shipIndex);
        }
       
    }


        

}
