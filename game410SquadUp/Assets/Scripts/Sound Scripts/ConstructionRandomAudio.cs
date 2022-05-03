using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionRandomAudio : MonoBehaviour
{
    float randomSound = 10f;
    int Randomizer = 0;
    float longSoundSpacer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        randomSound -= Time.deltaTime;
        longSoundSpacer -= Time.deltaTime;

        if (randomSound <= 0)
        {
            RandomSound();
            randomSound = Random.Range(6, 20);
        }
    }

    public void RandomSound()
    {
        Randomizer = Random.Range(1, 3);
        Debug.Log("Random Number:" + Randomizer);
        if (Randomizer == 1)
        {
            FindObjectOfType<AudioManager>().Play("ConstructionBG1");
        }

        if (Randomizer == 2)
        {
            FindObjectOfType<AudioManager>().Play("ConstructionBG2");
        }
        if (Randomizer == 3)
        {
            if (longSoundSpacer <= 0)
            {
                FindObjectOfType<AudioManager>().Play("ConstructionBG3");
            }
            longSoundSpacer = 30;
        }

        Randomizer = 0;
    }
}
