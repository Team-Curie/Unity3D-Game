using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseControllerScript : MonoBehaviour
{
    public bool isPaused;
    public float playerHealthCheck;
    public float shipHealthCheck;

    void Start()
    {
        //playerHealthCheck = PlayerPrefs.GetFloat("playerHealth");
        //shipHealthCheck = PlayerPrefs.GetFloat("shipHealth");
        isPaused = false;
    }

    void Update()
    {

    }

    public void PauseGameButtonClick(GameObject pauseMenu)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame(pauseMenu);
        }
        else if (!isPaused)
        {
            ResumeGame(pauseMenu);
        }
    }

    public void PauseGame(GameObject menu)
    {
        playerHealthCheck = PlayerPrefs.GetFloat("playerHealth");
        shipHealthCheck = PlayerPrefs.GetFloat("shipHealth");

        isPaused = true;

        Time.timeScale = 0;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        menu.SetActive(true);
    }

    public void ResumeGame(GameObject menu)
    {
        if (playerHealthCheck != 0 && shipHealthCheck != 0)
        {
            isPaused = false;
            Time.timeScale = 1;
            //Cursor.lockState = CursorLockMode.Confined;
            //Cursor.visible = true;
            menu.SetActive(false);
        }
    }

    public void StartNewGame(GameObject menu)
    {
        isPaused = false;
        Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        menu.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }

    public void Quit(GameObject menu)
    {
        isPaused = false;
        Time.timeScale = 1;
        menu.SetActive(false);
        Application.Quit();
    }
}
