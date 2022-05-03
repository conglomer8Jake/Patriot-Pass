using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionAudio : MonoBehaviour
{
    public bool constBool = false;

    public float introTimer = 11.03f;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("ConstructionIntro");
        constBool = false;
    }
    // Update is called once per frame
    void Update()
    {
        introTimer -= Time.deltaTime;
        if (introTimer <= 0  && constBool == false)
        {
            ConstructionLoop();
            constBool = true;
        }
    }
    
    public void ConstructionLoop()
    {
        FindObjectOfType<AudioManager>().Play("ConstructionLoop");
    }
    
}
