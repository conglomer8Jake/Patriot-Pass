using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CanvasManager : MonoBehaviour
{


    public GameObject UIClose;
    public GameObject UIOpen;

    /* //use below to stun the scene
    private float timeToDisable = **f;
    */
    private void Start()
    {
        GetComponent<Button>().interactable = false;

        //Invoke("EnableButton", timeToDisable);

    }

    public void NextCanvas()
    {
        if (UIOpen != null)
        {
            UIOpen.SetActive(true);
        }
        if (UIClose != null)
        {
            UIClose.SetActive(false);
        }
    }

    public void EnableButton()
    {
        GetComponent<Button>().interactable = true;
    }

}

