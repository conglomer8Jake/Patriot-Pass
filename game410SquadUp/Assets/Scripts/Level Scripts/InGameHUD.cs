using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameHUD : MonoBehaviour
{
    // This score text will display on the right side
    public Text rightScoreText;
    // This score text will display on the left side
    public Text leftScoreText;

    public GUISkin HUD;

    public Texture2D playerImage1;
    public Texture2D playerImage2;
    public Texture2D playerImage3;
    public Texture2D playerImage4;

    public TextMeshProUGUI playerName1;
    public TextMeshProUGUI playerName2;
    public TextMeshProUGUI playerName3;
    public TextMeshProUGUI playerName4;


    public static int scoreAmount;
    public int Score;


    // Start is called before the first frame update
    void Start()
    {

        scoreAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {

        //rightScoreText.text = scoreAmount;

    }

    void OnGUI()
    {

        GUI.skin = HUD;

        GUI.Label(new Rect(Screen.width / 2, 50, 100, 75), "Score");
        GUI.Label(new Rect(Screen.width / 2, 450, 100, 75), "Time");

        // These will display the users choosen characters
        GUI.Box(new Rect(0, 0, 100, 100), playerImage1);
        GUI.Box(new Rect(0, Screen.height / 1.24f, 100, 100), playerImage2);
        GUI.Box(new Rect(Screen.width / 1.095f, 0, 100, 100), playerImage3);
        GUI.Box(new Rect(Screen.width / 1.095f, Screen.height / 1.24f, 100, 100), playerImage4);



    }

}
