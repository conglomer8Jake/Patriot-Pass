using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class CharSelect : MonoBehaviour
{
    public GameObject FirstSelected;
    public GameObject Index0;
    public GameObject Index1;
    public GameObject Index2;
    public GameObject Index3;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    int currPlayer = 0;
    int cursorIndex = 0;

    public GameObject firstSelectedGameObject
    {
        get { return FirstSelected; }
        set { FirstSelected = value; }
    }


    //Set cursor index
    public static UIBehaviour current
    {
        
        set
        {
            
            int cursorIndex = 0;

            

            if (cursorIndex < 0)
            {
                cursorIndex = 3;
            }

            if (cursorIndex >= 4)
            {
                cursorIndex = 0;
            }

            if (cursorIndex == 0)
            {
                //Cursor.SetCursor = (cursortexture, hotspot, cursormode);
            }

        }

    }

    /*
    //How to assign GameObject Index0 = cursorindex0 ?
    private void Update()
    {
        if (charselection = "0")
        {
            GameObject.FindGameObjectWithTag("Index0");
        }
        if (charselection = "1")
        {
            GameObject.FindGameObjectWithTag("Index1");
        }
        if (charselection = "2")
        {
            GameObject.FindGameObjectWithTag("Index2");
        }
        if (charselection = "3")
        {
            GameObject.FindGameObjectWithTag("Index3");
        }
    }
    */


    //set cursor to gameobject?
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    //cursor move from 0~3, press fire1(left ctrl) to select
    void charselection()
    {

        if(Input.GetKeyDown("W"))
        {
            cursorIndex --;
            Debug.Log("cursor up");
        }

        if (Input.GetKeyDown("S"))
        {
            cursorIndex ++;
            Debug.Log("cursor down");
        }
        if (Input.GetKeyDown("left ctrl"))
        {
            Debug.Log("selected");
        }
    }

    //player 1,2 character selection
    void charbox()
    {
        if(cursorIndex ==0)
        {
            currPlayer = 0;
        }
        else if (cursorIndex == 1)
        {
            currPlayer = 1;
        }
    }



}
