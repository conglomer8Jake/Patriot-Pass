using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    int SarahRandomizer = 0;

    //Place this on every character with an animator


    public void StarUlt1()
    {
        FindObjectOfType<AudioManager>().Play("StarUlt1");
    }
    public void StarUlt2()
    {
        FindObjectOfType<AudioManager>().Play("StarUlt2");
    }
    public void StarUlt3()
    {
        FindObjectOfType<AudioManager>().Play("StarUlt3");
    }
    
    
    
    public void SarahUlt1()
    {
        FindObjectOfType<AudioManager>().Play("SarahUlt1");
    }
    public void SarahUlt2()
    {
        FindObjectOfType<AudioManager>().Play("SarahUlt2");
    }
    public void SarahUlt3()
    {
        
        SarahRandomizer = Random.Range(1, 4);
        if(SarahRandomizer == 1)
        {
            FindObjectOfType<AudioManager>().Play("SarahUlt3a");
        }
        if (SarahRandomizer == 2)
        {
            FindObjectOfType<AudioManager>().Play("SarahUlt3b");
        }
        if (SarahRandomizer == 3)
        {
            FindObjectOfType<AudioManager>().Play("SarahUlt3c");
        }
        if (SarahRandomizer == 4)
        {
            FindObjectOfType<AudioManager>().Play("SarahUlt3d");
        }
        SarahRandomizer = 0;
    }

    //For new characters just copy and paste what is above and change void names and audio clips

}
