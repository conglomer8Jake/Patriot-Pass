using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterHUD : MonoBehaviour
{
    public Texture2D sarahSprite;
    public Texture2D starshipSprite;
    public Texture2D garySprite;
    public Texture2D dietrichSprite;

    public TextMeshProUGUI sarahName;
    public TextMeshProUGUI starshipName;
    public TextMeshProUGUI garyName;
    public TextMeshProUGUI dietrichName;

    // Start is called before the first frame update
    void Start()
    {
        //if(global.p1=="sarah"){player1Sprite=sarahSprite; player1Name=sarahName} and so on
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
