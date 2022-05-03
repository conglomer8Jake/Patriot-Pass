using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(/*SceneManager.GetActiveScene().testScene1 && */Input.GetKeyDown(KeyCode.UpArrow))
        {
            MenuUp();
        }
        if (/*SceneManager.GetActiveScene().testScene1 && */Input.GetKeyDown(KeyCode.DownArrow))
        {
            MenuDown();
        }
    }

    void MenuUp()
    {
        FindObjectOfType<AudioManager>().Play("MenuUp");
    }
    void MenuDown()
    {
        FindObjectOfType<AudioManager>().Play("MenuDown");
    }
}
