using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timeText;

    public float timeRemaining;

    public bool timerIsRunning = false;

    [SerializeField] GameObject endMenu;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {

        /*if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            Debug.Log("Time is counting down!");
        }
        else
        {
            Debug.Log("Time has run out!");
            timeRemaining = 0;
            timerIsRunning = false;
            TimeUp();
        }*/
        TimeUp();
        DisplayTime(timeRemaining);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimeUp()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            Debug.Log("Time is counting down!");
        }
        else 
        {
            Time.timeScale = 0;
            timeRemaining = 0;
            timerIsRunning = false;
            endMenu.SetActive(true);
            Debug.Log("Time has run out!");
        }
    }

}
