using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIButtonManager : MonoBehaviour
{

    // Button controls
    public bool playButton = false;
    bool hasBeenPressed = false;
    bool settingButton = false;
    bool controlButton = false;
    bool mapUIButton = false;

    // Skins for the buttons
    public GUISkin skin;
    public GUISkin skinA;

    // For the different fonts
    public GUISkin skinFont;

    // For the different 2D textures
    public Texture2D titleImage = null;
    //public Texture2D menuBackground = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

        GUI.skin = skin;

        // Button system: If the user press any of the buttons then the layout of the menu will change
        if (!hasBeenPressed)
        {

            TitleBox();

            if (GUI.Button(new Rect(Screen.width / 50, 180, 300, 75), "Play"))
            {
                playButton = true;
                hasBeenPressed = true;
                print("The Button was clicked!");
            }

            if (GUI.Button(new Rect(Screen.width / 50, 260, 300, 75), "Settings"))
            {
                settingButton = true;
                hasBeenPressed = true;
                print("The Button was clicked!");
            }

            if (GUI.Button(new Rect(Screen.width / 50, 340, 300, 75), "How to Play"))
            {
                controlButton = true;
                hasBeenPressed = true;
                print("The Button was clicked!");
            }

            if (GUI.Button(new Rect(Screen.width / 50, 420, 300, 75), "Quit"))
            {
                Application.Quit();
                hasBeenPressed = true;
                print("The player quit the game!");
            }

        }

        if (playButton == true)
        {
            GUIback();
            GUIPlayButtonSkin();
        }

        if (mapUIButton == true)
        {

            GUIback();
            GUIMapUI();

        }

        if (settingButton == true)
        {

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 150, 50), "Volume"))
            {
                print("The Button was clicked!");
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 4, 150, 50), "Controls"))
            {
                print("The Button was clicked!");
            }

            GUIback();

        }

        if (controlButton == true)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 200), "Git Good!");
            GUIback();
        }



    }

    // This is the code for the back button
    void GUIback()
    {
        // If the user presses the back button then the screen will switch back to the previous layout. 
        if (GUI.Button(new Rect(Screen.width / 1.2f, Screen.height / 1.2f, 150, 50), "Back"))
        {
            print("The Button was clicked!");
            SceneManager.LoadScene("testScene1");
        }

    }

    void GUIPlayButtonSkin()
    {

        GUI.skin = skinA;

        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 8, 300, 400), "AI"))
        {
            print("The Button was clicked!");
            playButton = false;
            mapUIButton = true;
            //SceneManager.LoadScene("testScene2");
            GUIMapUI();

        }

        if (GUI.Button(new Rect(Screen.width / 5, Screen.height / 8, 300, 400), "Local"))
        {
            print("The Button was clicked!");
            playButton = false;
            mapUIButton = true;
            //SceneManager.LoadScene("testScene2");
            GUIMapUI();

        }

    }

    void GUIMapUI()
    {

        if (GUI.Button(new Rect(300, 50, 150, 150), "Map 1"))
        {
            print("The Button was clicked!");
            SceneManager.LoadScene("testScene2");
        }

        if (GUI.Button(new Rect(500, 50, 150, 150), "Map 2"))
        {
            print("The Button was clicked!");
            SceneManager.LoadScene("testScene3");
        }

        if (GUI.Button(new Rect(300, 250, 150, 150), "Map 3"))
        {
            print("The Button was clicked!");
            SceneManager.LoadScene("testScene4");
        }

        if (GUI.Button(new Rect(500, 250, 150, 150), "Map 4"))
        {
            print("The Button was clicked!");
            SceneManager.LoadScene("testScene5");
        }

    }

    // The title of the game. 
    void TitleBox()
    {
        GUI.Box(new Rect(0, 0, 300, 150), titleImage);
    }

}
