using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    //Use "GameIsPaused to change pitch in the AudioManager script.
    /*e.g. 
    if (PauseMenu.GameIsPaused)
    {
        s.source.pitch *= .5f;
    }

     */
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    //These Game Object are for the selected buttons
    public GameObject ResumeButton;
    public GameObject ReplayButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        //Clears the selected button from the Event System
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new selected button for the Event System
        EventSystem.current.SetSelectedGameObject(ReplayButton);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        //Clears the selected button from the Event System
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new selected button for the Event System
        EventSystem.current.SetSelectedGameObject(ResumeButton);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }    

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TestScene1");
        Debug.Log("Loading Menu.....");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game.....");
        Application.Quit();
    }
}
