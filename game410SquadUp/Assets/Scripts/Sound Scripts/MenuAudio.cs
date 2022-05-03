using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public float introTimer = 60.3f;

    public bool menuBool = false;

    void Awake()
    {
        Invoke("startSoudns", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<AudioManager>().Stop("EndMatchLoop");
        FindObjectOfType<AudioManager>().Stop("EndMatchIntro");
        FindObjectOfType<AudioManager>().Stop("ConstructionLoop");
        FindObjectOfType<AudioManager>().Stop("ConstructionIntro");
        introTimer -= Time.deltaTime;
        if (introTimer <= 0 && menuBool == false)
        {
            FindObjectOfType<AudioManager>().Play("MenuLoop");
            menuBool = true;
        }
    }
    public void startSoudns()
    {
        FindObjectOfType<AudioManager>().Play("MenuIntro");
        menuBool = false;
    }
}
