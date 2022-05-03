using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;



public class PlayerNumber : MonoBehaviour
{
    public Global global;
    public static event Action<int, string> characterDelegator;
    //Q1 public List<int>  cursorIndex;(can this work?..)
    //Q2 how to assign index 0 = gameobject(such as vs1,garybutton,etc)

    //for the first selected gameobject(vs1)
    public Image vs1;
    public Image vs2;
    private Image CurrentSelection;
    public Image CurrentSelection1;
    public Image CurrentSelection2;


    [Range(0, 1)]
    int playerCountIndex = 0;

    int cursorIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        CurrentSelection = CurrentSelection1;
    }
    // Update is called once per frame
    void Update()
    {
        InputChecker();
        if (cursorIndex == 0)
        {
            vs1.gameObject.SetActive(true);
            vs2.gameObject.SetActive(false);
        }
        else if (cursorIndex == 1)
        {
            vs1.gameObject.SetActive(false);
            vs2.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            CurrentSelection.gameObject.SetActive(true);
                if (cursorIndex == 0)
                {
                    CurrentSelection.sprite = vs1.sprite;
                    global.numPlayers = 2;
                    //characterDelegator?.Invoke(playerCountIndex, "1VS1");
                }
                if (cursorIndex == 1)
                {
                    CurrentSelection.sprite = vs2.sprite;
                    global.numPlayers = 4;
                    //characterDelegator?.Invoke(playerCountIndex, "2VS2");
                }

                playerCountIndex++;
            if (playerCountIndex >= 1)
            {
                CurrentSelection = CurrentSelection2;
                LoadScene();
            }
            
           Debug.Log("selected");
        }
    }
    public void InputChecker()
    {
        if (playerCountIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                cursorIndex--;
                Debug.Log("cursor left");
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                cursorIndex++;
                Debug.Log("cursor right");
            }
        } else if (playerCountIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                cursorIndex--;
                Debug.Log("cursor left");
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                cursorIndex++;
                Debug.Log("cursor right");
            }
        } 
        //Alternative way of using Mathf.Clamp
        /*if(cursorIndex < 0)
        { 
            cursorIndex = 0;
        }
        if (cursorIndex > 3)
        {
            cursorIndex = 3;
        }*/
        cursorIndex = Mathf.Clamp(cursorIndex, 0, 1);
    }
    public void LoadScene()
    {
        //Send player to map selection scene
        SceneManager.LoadScene("ChampSelect");
    }
}
