using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public GameObject creditsScreen;
    
	public void loadMainScene()
    {
        
        Application.LoadLevel("Hangar");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void showCredits()
    {
        creditsScreen.SetActive(true);

    }
    public void hideCredits()
    {
        creditsScreen.SetActive(false);
    }
}
