using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{
    public Global global;

    public int combo;
    public int leftScore, rightScore;
    private int targetScore = 25;

    public TextMeshProUGUI finalScoreText;

    public GameObject starshipPrefab, sarahPrefab, dieterichPrefab, garyPrefab;
    public GameObject playerToSpawn;
    public GameObject leftSpawner, rightSpawner;
    public GameObject[] players;
    public GameObject[] lokiList; 
    public List<GameObject> teamOne, teamTwo;

    public Text timeText;
    public float timeRemaining;
    public bool timerIsRunning = false;

    public float introTimer = 10.9f;

    public bool timerBool = false;
    public bool timerIntroBool = false;
    public bool scoreBool = false;
    public bool scoreIntroBool = false;

    [SerializeField] GameObject endMenu;
    [SerializeField] GameObject team1Win;
    [SerializeField] GameObject team2Win;
    [SerializeField] GameObject tieMenu;

    //I'm so sorry about this :(
    public Image player1Sprite;
    public Image player2Sprite;
    public Image winningPlayerSprite;

    public TextMeshProUGUI player1Name;
    public TextMeshProUGUI player2Name;

    public Sprite sarahSprite;
    public Sprite starshipSprite;
    public Sprite garySprite;
    public Sprite dietrichSprite;

    public string sarahName;
    public string starshipName;
    public string garyName;
    public string dietrichName;

    void Start()
    {
        findSpawners();
        spawnPlayers();
        players = GameObject.FindGameObjectsWithTag("Player");
        Time.timeScale = 1;
        timerIsRunning = true;
        setSpriteImages();
        Invoke("AssignLokis", 0.5f);
        Invoke("AssignTeams", 0.5f);
        Invoke("AssignPlayerNumber", 0.5f);
    }
    private void OnEnable()
    {
        DiskMovement.puckCatch += comboCount;
        puckScript.score += resetComboCount;
    }
    private void OnDisable()
    {
        DiskMovement.puckCatch -= comboCount;
        puckScript.score -= resetComboCount;
    }
    void Update()
    {
        introTimer -= Time.deltaTime;
        DisplayTime(timeRemaining);
        if (rightScore >= targetScore)
        {
            Time.timeScale = 0;
            timeRemaining = 0;
            endMenu.SetActive(true);
            team2Win.SetActive(true);
            winningPlayerSprite.sprite = player2Sprite.sprite;
            finalScoreText.text = rightScore.ToString();
            Debug.Log("Team 1 wins!");

            FindObjectOfType<AudioManager>().Stop("ConstructionLoop");
            if (timerIntroBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchIntro");
                timerIntroBool = true;
            }
                
            if (introTimer <= 0 && timerBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchLoop");
                timerBool = true;
            }
        }
        else if (leftScore >= targetScore)
        {
            Time.timeScale = 0;
            timeRemaining = 0;
            endMenu.SetActive(true);
            team1Win.SetActive(true);
            winningPlayerSprite.sprite = player1Sprite.sprite;
            finalScoreText.text = leftScore.ToString();
            Debug.Log("Team 2 wins!");

            FindObjectOfType<AudioManager>().Stop("ConstructionLoop");
            if (timerIntroBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchIntro");
                timerIntroBool = true;
            }
            if (introTimer <= 0 && timerBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchLoop");
                timerBool = true;
            }
        }
        TimeUp();
    }

    // This will show the timer in a certain format. 
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    // When the timr runs out, one of the following will be true if the coniditions are met. One of the team will win if they have the higher score or the game will result in a tie if they have the same score (For now).
    void TimeUp()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining <= 0 && rightScore == leftScore)
        {
            Debug.Log("Tie!");
            endMenu.SetActive(true);
            TimeStop();

            FindObjectOfType<AudioManager>().Stop("ConstructionLoop");
            if (scoreIntroBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchIntro");
                scoreIntroBool = true;
            }
            if (introTimer <= 0 && scoreBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchLoop");
                scoreBool = true;
            }
        }
        else if (timeRemaining <= 0 && rightScore > leftScore)
        {
            endMenu.SetActive(true);
            team2Win.SetActive(true);
            finalScoreText.text = rightScore.ToString();
            Debug.Log("Team 2 wins!");
            TimeStop();

            FindObjectOfType<AudioManager>().Stop("ConstructionLoop");
            if (scoreIntroBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchIntro");
                scoreIntroBool = true;
            }
            if (introTimer <= 0 && scoreBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchLoop");
                scoreBool = true;
            }
        }
        else if (timeRemaining <= 0 && leftScore > rightScore)
        {
            endMenu.SetActive(true);
            team1Win.SetActive(true);
            finalScoreText.text = leftScore.ToString();
            Debug.Log("Team 1 wins!");
            TimeStop();

            FindObjectOfType<AudioManager>().Stop("ConstructionLoop");
            if (scoreIntroBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchIntro");
                scoreIntroBool = true;
            }
            if (introTimer <= 0 && scoreBool == false)
            {
                FindObjectOfType<AudioManager>().Play("EndMatchLoop");
                scoreBool = true;
            }
        }
    }
    // This will stop the game if the time runs out. 
    void TimeStop()
    {
        Time.timeScale = 0;
        timeRemaining = 0;
        timerIsRunning = false;
        endMenu.SetActive(true);
        Debug.Log("Time has run out!");

        /*FindObjectOfType<AudioManager>().Stop("ConstructionLoop");
        FindObjectOfType<AudioManager>().Play("EndMatchIntro");
        if (introTimer <= 0)
        {
            FindObjectOfType<AudioManager>().Play("EndMatchLoop");
        }*/
    }
    public void comboCount()
    {
        combo += 1;
    }
    public void resetComboCount()
    {
        combo = 0;
    }

    public void setSpriteImages()
    {
        //find which players and determine which is 1 and 2 based on location
        GameObject player1;
        GameObject player2;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if(players[0].transform.position.x<0)
        {
            player1 = players[0];
            player2 = players[1];
        }
        else
        {
            player1 = players[1];
            player2 = players[0];
        }

        //if a character specific script is found on one, their name and sprite will be reflected in the UI
        if(player1.GetComponent("sarahAbilityHandler"))
        {
            player1Sprite.sprite = sarahSprite;
            player1Name.text = sarahName;
        }
        else if (player1.GetComponent("starshipAbilityHandler"))
        {
            player1Sprite.sprite = starshipSprite;
            player1Name.text = starshipName;
        }
        else if (player1.GetComponent("dieterichAbilityHandler"))
        {
            player1Sprite.sprite = dietrichSprite;
            player1Name.text = dietrichName;
        }
        else if (player1.GetComponent("garymungerAbilityHandler"))
        {
            player1Sprite.sprite = garySprite;
            player1Name.text = garyName;
        }

        if (player2.GetComponent("sarahAbilityHandler"))
        {
            player2Sprite.sprite = sarahSprite;
            player2Name.text = sarahName;
        }
        else if (player2.GetComponent("starshipAbilityHandler"))
        {
            player2Sprite.sprite = starshipSprite;
            player2Name.text = starshipName;
        }
        else if (player2.GetComponent("dieterichAbilityHandler"))
        {
            player2Sprite.sprite = dietrichSprite;
            player2Name.text = dietrichName;
        }
        else if (player2.GetComponent("garymungerAbilityHandler"))
        {
            player2Sprite.sprite = garySprite;
            player2Name.text = garyName;
        }
    }
    public void spawnPlayers()
    {
        for (int i = 0; i < global.numPlayers; i++)
        {
            if (i == 0)
            {
                ConvertStringToGameObject(i);
                Instantiate(playerToSpawn, leftSpawner.transform.position, Quaternion.Euler(0.0f, 90.0f, -90.0f));
            } else if (i == 1)
            {
                ConvertStringToGameObject(i);
                Instantiate(playerToSpawn, rightSpawner.transform.position, Quaternion.Euler(180.0f, 90.0f, -90.0f));
            }
        }
    }
    public void ConvertStringToGameObject(int i)
    {
        if (global.players[i] == "Sarah")
        {
            playerToSpawn = sarahPrefab;
        } else if (global.players[i] == "Starship")
        {
            playerToSpawn = starshipPrefab;
        } else if (global.players[i] == "Gary")
        {
            playerToSpawn = garyPrefab;
        } else if (global.players[i] == "Dieterich")
        {
            playerToSpawn = dieterichPrefab;
        }
    }
    public void findSpawners()
    {
        leftSpawner = GameObject.FindGameObjectWithTag("leftSpawner");
        rightSpawner = GameObject.FindGameObjectWithTag("rightSpawner");
    }
    public void AssignLokis()
    {
        //Check if multiple sarahs
        //Make a list of Loki objects in scene, assign them to player One/Two
        for (int i = 0; i < players.Length; i++)
        {
            lokiList = GameObject.FindGameObjectsWithTag("loki");
            if (players[i].GetComponent<playerMovementHandler>().name == "Sarah_Character(Clone)")
            {
                lokiList[0].GetComponent<lokiFunctionalityHandler>().sarahPlayer = players[i];
            } else
            {
                lokiList[0].GetComponent<lokiFunctionalityHandler>().opposingPlayer = players[i];
            }
        }
        if (players[0].name == "Sarah_Character(Clone)" && players[1].name == "Sarah_Character(Clone)")
        {
            Debug.Log("two Sarah");
            lokiList = GameObject.FindGameObjectsWithTag("loki");
            for (int i = 0; i < lokiList.Length; i++)
            {
                if (i == 0)
                {
                    lokiList[i].GetComponent<lokiFunctionalityHandler>().sarahPlayer = players[0];
                    lokiList[i].GetComponent<lokiFunctionalityHandler>().opposingPlayer = players[1];
                }
                else if (i == 1)
                {
                    lokiList[i].GetComponent<lokiFunctionalityHandler>().sarahPlayer = players[1];
                    lokiList[i].GetComponent<lokiFunctionalityHandler>().opposingPlayer = players[0];
                }
            }
        }
    }
    public void AssignTeams()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<playerMovementHandler>().playerSide == "left")
            {
                teamOne.Add(players[i]);
                players[i].GetComponent<playerMovementHandler>().Team = playerMovementHandler.team.one;
            }
            if (players[i].GetComponent<playerMovementHandler>().playerSide == "right")
            {
                teamTwo.Add(players[i]);
                players[i].GetComponent<playerMovementHandler>().Team = playerMovementHandler.team.two;
            }
        }
    }
    public void AssignPlayerNumber()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (i == 0)
            {
                players[i].GetComponent<playerMovementHandler>().currentPlayer = playerMovementHandler.player.one;
            } else if (i == 1)
            {
                players[i].GetComponent<playerMovementHandler>().currentPlayer = playerMovementHandler.player.two;
            } else if (i == 2)
            {
                players[i].GetComponent<playerMovementHandler>().currentPlayer = playerMovementHandler.player.three;
            } else if (i == 3)
            {
                players[i].GetComponent<playerMovementHandler>().currentPlayer = playerMovementHandler.player.four;
            }
        }
    }
}
