using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerAbilityHandler : MonoBehaviour
{
    public static event Action<bool> playerThrowUpdate;
    //public static event Action<bool> playerThrowUpdateSL;
    public static event Action<bool> playerUlt2Update;
    public static event Action<bool> playerUlt1Update;
    public static event Action<bool> playerUlt3Update;
  
    public static event Action playerSpikeUpdate;
    //Ult windups: 1bar = 0.5sec, 2bar = 1.0sec, 3bar = 1.5sec
    public GameObject officerPrefab;
    public GameObject sandstorm;
    public GameObject sarahTimeDistort;
    public GameObject lokiPrefab, lokiGame;
    public GameObject starshipUltPrefab;
    public GameObject opposingPlayer;

    public gameManager gM;
    public DiskMovement dM;
    public enum playerCharacter
    {
        starship, garyMUnger, sarahLoki, Dieterich
    }
    public GameObject garySquadPrefab;
    public lokiFunctionalityHandler lFH;

    public playerCharacter pC;
    public playerMovementHandler pMH;
    public float timeCaughtElapsed;
    public float buttonPressedElapsed;
    public bool maxTimeCaught = false;
    public float offsetX;
    public float officerOffsetY;
    public float ultBar;
    public float ultMod = 1.0f;
    void Start()
    {
        pMH = this.gameObject.GetComponent<playerMovementHandler>();
        gM = GameObject.FindObjectOfType<gameManager>();
        dM = GameObject.FindGameObjectWithTag("Puck").GetComponent<DiskMovement>();
        checkSide();
        Invoke("assignOpposingPlayer", 0.5f);
        if (pC == playerAbilityHandler.playerCharacter.sarahLoki)
        {
            Instantiate(lokiPrefab, new Vector3(this.gameObject.transform.position.x + offsetX, this.gameObject.transform.position.y, 0), Quaternion.identity);
            lokiGame = GameObject.FindGameObjectWithTag("loki");
            lFH = lokiGame.GetComponent<lokiFunctionalityHandler>();
            Instantiate(sarahTimeDistort, this.gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
        }
        else
        {
        }
       
    }
    private void FixedUpdate()
    {
        if (pMH.playerState == "puckState" && !maxTimeCaught)
        {
            timeCaughtElapsed += Time.deltaTime;
        }
        if (timeCaughtElapsed >= 5.0f)
        {
            maxTimeCaught = true;
            timeCaughtElapsed = 5.0f;
        }
        if (Input.GetKey(KeyCode.X))
        {
            buttonPressedElapsed += Time.deltaTime;
        }
    }
    void Update()
    {
        if (ultBar <= 3)
        {
            ultBar += 0.1f * Time.deltaTime * ultMod;
        }
        if (ultBar > 3)
        {
            ultBar = 3;
        }
        if (pMH.playerState == "fieldState")
        {
            {
                if (pMH.GetButtonUltDown())
                {
                    if (pC == playerCharacter.starship)
                    {
                        if (ultBar >= 3)
                        {
                            playerUlt1Update?.Invoke(true);
                            LaunchUlt3();
                            decreaseUltBar(3);
                        } else if (ultBar <= 2 && ultBar >= 1)
                        {
                            playerUlt1Update?.Invoke(true);
                            LaunchUlt1();
                            decreaseUltBar(1);
                        }
                    }
                    if (pC == playerCharacter.sarahLoki)
                    {
                        if (ultBar >= 3)
                        {
                            playerUlt3Update?.Invoke(true);
                            LaunchUlt3();
                            decreaseUltBar(3);
                        }
                        else if (ultBar >= 2)
                        {
                            playerUlt2Update?.Invoke(true);
                            LaunchUlt2();
                            decreaseUltBar(2);
                        }
                        else if (ultBar >= 1)
                        {
                            playerUlt1Update?.Invoke(true);
                            LaunchUlt1();
                            decreaseUltBar(1);
                        }
                    }
                    if (pC == playerCharacter.Dieterich && ultBar >= 3)
                    {
                            LaunchUlt3();
                            decreaseUltBar(3);
                        
                    }
                }
            }
        }
        else if (pMH.playerState == "puckState")
        {
            {
                if (pMH.GetButtonThrowDown())
                {
                    if (timeCaughtElapsed <= 0.5f)
                    {   
                        dM.parryDisc();
                        pMH.canDash = false;
                        Invoke("callResetDash", 0.5f);
                        playerThrowUpdate?.Invoke(true);
                        Debug.Log("Help");
                    } else
                    {
                        dM.throwDisc();
                        pMH.canDash = false;
                        Invoke("callResetDash", 0.5f);
                        playerThrowUpdate?.Invoke(true);
                    }
                }
                else if (pMH.GetButtonUltDown())
                {
                    if (pC == playerCharacter.starship)
                    {
                        if (ultBar >= 3)
                        {
                            LaunchUlt3();
                            decreaseUltBar(3);
                        }
                        else if (ultBar >= 2)
                        {
                            LaunchUlt2();
                            playerUlt2Update?.Invoke(true);
                            decreaseUltBar(2);
                        }
                    }
                    if (pC == playerCharacter.garyMUnger)
                    {
                        if (ultBar >= 3)
                        {
                            playerUlt3Update?.Invoke(true);
                            LaunchUlt3();
                            decreaseUltBar(3);
                        }
                        else if (ultBar >= 2)
                        {
                            playerUlt2Update?.Invoke(true);
                            LaunchUlt2();
                            decreaseUltBar(2);
                        }
                        else if (ultBar >= 1)
                        {
                            playerUlt1Update?.Invoke(true);
                            LaunchUlt1();
                            decreaseUltBar(1);
                        }
                    }
                    if (pC == playerCharacter.sarahLoki)
                    {
                        if (ultBar >= 3)
                        {
                            playerUlt3Update?.Invoke(true);
                            LaunchUlt3();
                            decreaseUltBar(3);
                        }
                        else if (ultBar >= 2)
                        {
                            playerUlt2Update?.Invoke(true);
                            LaunchUlt2();
                            decreaseUltBar(2);
                        }
                        else if (ultBar >= 1)
                        {
                            playerUlt1Update?.Invoke(true);
                            LaunchUlt1();
                            decreaseUltBar(1);
                        }
                    }
                    if (pC == playerCharacter.Dieterich)
                    {
                        if (ultBar >= 3)
                        {
                            playerUlt3Update?.Invoke(true);
                            LaunchUlt3();
                            decreaseUltBar(3);
                        }
                        if (ultBar >= 2)
                        {
                            playerUlt2Update?.Invoke(true);
                            LaunchUlt2();
                            decreaseUltBar(2);
                        }
                        else if (ultBar >= 1)
                        {
                            playerUlt1Update?.Invoke(true);
                            LaunchUlt1();
                            decreaseUltBar(1);
                        }
                    }
                }
            }
        }
    }
    public void assignOpposingPlayer()
    {
        for (int i = 0; i < gM.players.Length; i++)
        {
            if (gM.players[i].GetComponent<playerMovementHandler>().Team != this.gameObject.GetComponent<playerMovementHandler>().Team)
            {
                opposingPlayer = gM.players[i];
                return;
            }
        }
    }
    public void checkSide()
    {
        if (pMH.playerSide == "left")
        {
            offsetX = 2.0f;
        } else if (pMH.playerSide == "right")
        {
            offsetX = -2.0f;
        }
    }
    public void decreaseUltBar(int bar)
    {
        ultBar -= bar;
    }
    public void inhibitUltGain()
    {
        ultMod = 0.75f;
        Invoke("resetUltGain", 2.0f);
    }
    public void resetUltGain()
    {
        ultMod = 1.0f;
    }
    public void callResetSpeed()
    {
        pMH.resetSpeed();
    }
    public void callResetDash()
    {
        pMH.resetDash();
    }
    protected virtual void LaunchUlt1()
    {
    }
    protected virtual void LaunchUlt2()
    {
    }
    protected virtual void LaunchUlt3()
    {
    }
}