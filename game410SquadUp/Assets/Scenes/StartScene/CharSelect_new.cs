using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class CharSelect_new : MonoBehaviour
{
    public Global global;
    public static event Action<int, string> characterDelegator;
    //Q1 public List<int>  cursorIndex;(can this work?..)
    //Q2 how to assign index 0 = gameobject(such as sarahbutton,garybutton,etc)

    //for the first selected gameobject(SarahButton)
    public Image SarahButton;
    public Image StarShipButton;
    public Image GaryButton;
    public Image ProfButton;
    private Image CurrentSelection;
    public Image CurrentSelection1;
    public Image CurrentSelection2;
    public Image CurrentSelection3;
    public Image CurrentSelection4;

    [Range(0, 4)]
    int playerIndex = 0;

    //unity clamp
    //public static float Clamp(float playerIndex, float 0, float 4);

    //private int[] playerIndexUp = new int[] { 1, 2, 3, 4 };
    //private int[] playerIndexDown = new int[] { 0, 1, 2, 3 };

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
            SarahButton.gameObject.SetActive(true);
            StarShipButton.gameObject.SetActive(false);
            GaryButton.gameObject.SetActive(false);
            ProfButton.gameObject.SetActive(false);
        }
        else if (cursorIndex == 1)
        {
            SarahButton.gameObject.SetActive(false);
            StarShipButton.gameObject.SetActive(true);
            GaryButton.gameObject.SetActive(false);
            ProfButton.gameObject.SetActive(false);
        }
        else if (cursorIndex == 2)
        {
            SarahButton.gameObject.SetActive(false);
            StarShipButton.gameObject.SetActive(false);
            GaryButton.gameObject.SetActive(true);
            ProfButton.gameObject.SetActive(false);
        }
        else if (cursorIndex == 3)
        {
            SarahButton.gameObject.SetActive(false);
            StarShipButton.gameObject.SetActive(false);
            GaryButton.gameObject.SetActive(false);
            ProfButton.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.BackQuote) && playerIndex == 0)
        {
            CurrentSelection.gameObject.SetActive(true);
                if (cursorIndex == 0)
                {
                    CurrentSelection.sprite = SarahButton.sprite;
                    characterDelegator?.Invoke(playerIndex, "Sarah");
                }
                if (cursorIndex == 1)
                {
                    CurrentSelection.sprite = StarShipButton.sprite;
                    characterDelegator?.Invoke(playerIndex, "Starship");
                }
                if (cursorIndex == 2)
                {
                    CurrentSelection.sprite = GaryButton.sprite;
                    characterDelegator?.Invoke(playerIndex, "Gary");
                }
                if (cursorIndex == 3)
                {
                    CurrentSelection.sprite = ProfButton.sprite;
                    characterDelegator?.Invoke(playerIndex, "Dieterich");
                }
                playerIndex++;
        }
        if (Input.GetKeyUp(KeyCode.Period) && playerIndex == 1)
        {
            CurrentSelection = CurrentSelection2;
            if (cursorIndex == 0)
            {
                CurrentSelection.sprite = SarahButton.sprite;
                characterDelegator?.Invoke(playerIndex, "Sarah");
            }
            if (cursorIndex == 1)
            {
                CurrentSelection.sprite = StarShipButton.sprite;
                characterDelegator?.Invoke(playerIndex, "Starship");
            }
            if (cursorIndex == 2)
            {
                CurrentSelection.sprite = GaryButton.sprite;
                characterDelegator?.Invoke(playerIndex, "Gary");
            }
            if (cursorIndex == 3)
            {
                CurrentSelection.sprite = ProfButton.sprite;
                characterDelegator?.Invoke(playerIndex, "Dieterich");
            }
            playerIndex++;
        }
        if (playerIndex == 2)
        {
            if (global.numPlayers == 2)
            {
                LoadScene();
                Debug.Log("changeScene");
            }
            CurrentSelection = CurrentSelection3;
        }
        if (playerIndex == 3)
        {
            CurrentSelection = CurrentSelection4;
        }
        if (playerIndex >= 4)
        {
            LoadScene();
            Debug.Log("changeScene");
        }
        //To-do list
        //(JACOB)send information(character selection information) to the main game scene.
        //(JACOB)change input based on playerindex(in the character selection scene).
        //change the cursorIndex value <int.clamp>"UNITY CLAMP"_ Range?
        Debug.Log("selected");
    }
    public void InputChecker()
    {
        if (playerIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                cursorIndex--;
                Debug.Log("cursor up");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                cursorIndex++;
                Debug.Log("cursor down");
            }
        } else if (playerIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                cursorIndex--;
                Debug.Log("cursor up");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                cursorIndex++;
                Debug.Log("cursor down");
            }
        } else if (playerIndex == 2)
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                cursorIndex--;
                Debug.Log("cursor up");
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                cursorIndex++;
                Debug.Log("cursor down");
            }
        } else if (playerIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                cursorIndex--;
                Debug.Log("cursor up");
            }

            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                cursorIndex++;
                Debug.Log("cursor down");
            }
        }

        /*if(cursorIndex < 0)
        { 
            cursorIndex = 0;
        }
        if (cursorIndex > 3)
        {
            cursorIndex = 3;
        }*/
        cursorIndex = Mathf.Clamp(cursorIndex, 0, 3);
    }
    public void LoadScene()
    {
        //Send player to map selection scene
        SceneManager.LoadScene("StartScene2");
    }
}
