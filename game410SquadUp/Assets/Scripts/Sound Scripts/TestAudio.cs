using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    //Find the AudioManager and play sound "Test"
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Test");
    }

}
