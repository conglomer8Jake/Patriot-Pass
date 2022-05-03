using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardSlider : MonoBehaviour
{
    public Slider mainSlider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            mainSlider.value -=  0.01f;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mainSlider.value += 0.01f;
        }
    }
}
