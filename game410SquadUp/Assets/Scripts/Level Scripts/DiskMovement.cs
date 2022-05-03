using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DiskMovement : MonoBehaviour
{
    public static event Action puckCatch;
    playerMovementHandler pmh;
    playerAbilityHandler pah;
    public GameObject puckVisage;
    public GameObject starshipUltPrefab;
    public GameObject[] players;
    public GameObject notGary;
    public GameObject playerCollided;
    public GameObject playerEmpty;
    public Transform playerCollidedEmpty;
    public Rigidbody rb;

    private int changeYIt;

    public string diskState;
    private float slerpSpeed = 0.25f;
    public float diskSpeed;
    public float baseDiskSpeed = 0.1f;
    public bool isHolding = false;
    public bool angling;
    public bool gravityEnabled, recentGravManip;
    public bool recentlyCarried;
    public bool gravityManip = false;

    public Vector3 addedThrowVector;
    public Vector3 initThrowVector;
    public Vector3 targetThrowVector;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        diskState = "thrown";
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int j = 0; j < players.Length; j++)
        {
            if (players[j].GetComponent<playerAbilityHandler>().pC != playerAbilityHandler.playerCharacter.garyMUnger)
            {
                notGary = players[j];
            }
        }
    }
    void Update()
    {
        if (diskState == "caught")
        {
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            FindObjectOfType<AudioManager>().Play("Catch");
            angling = true;
            this.gameObject.transform.position = playerCollidedEmpty.position;
            diskSpeed = 0.0f;
        }
        if (diskState == "starshipUltState")
        {
            /*
            starshipUltPrefab = GameObject.FindGameObjectWithTag("starshipUlt");
            this.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            this.gameObject.transform.position = new Vector3(starshipUltPrefab.transform.position.x, starshipUltPrefab.transform.position.y, 0.0f);
            recentlyCarried = true;
            */
        }
        if (angling)
        {
            aimingDirection();
        }
        if (gravityEnabled)
        {
            rb.AddForce(new Vector3(0, 0, 10.0f));
        }
        if (diskState == "thrown")
        {
            if (recentlyCarried)
            {
                this.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
                Invoke("addForceAfterCol", 1.5f);
                recentlyCarried = false;
            } else
            {

            }
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            puckCatch?.Invoke();
            pmh = other.gameObject.GetComponent<playerMovementHandler>();
            pah = other.gameObject.GetComponent<playerAbilityHandler>();
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            playerCollided = other.gameObject;
            if (pah.pC == playerAbilityHandler.playerCharacter.starship)
            {
                if (pmh.playerSide == "left")
                {
                    playerCollidedEmpty = playerCollided.transform.GetChild(3);
                }
                else
                {
                    playerCollidedEmpty = playerCollided.transform.GetChild(3);
                }
            }
            else if (pah.pC == playerAbilityHandler.playerCharacter.sarahLoki)
            {
                if (pmh.playerSide == "left")
                {
                    playerCollidedEmpty = playerCollided.transform.GetChild(3);
                }
                else
                {
                    playerCollidedEmpty = playerCollided.transform.GetChild(3);
                }
            }
            else if (pah.pC == playerAbilityHandler.playerCharacter.garyMUnger)
            {
                if (pmh.playerSide == "left")
                {
                    playerCollidedEmpty = playerCollided.transform.GetChild(3);
                }
                else
                {
                    playerCollidedEmpty = playerCollided.transform.GetChild(3);
                }
            }
            diskState = "caught";
            pmh.playerState = "puckState";
            //this.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("timeDistort") && gravityManip)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity /= 2;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("timeDistort") && recentGravManip)
        {
            //this.gameObject.GetComponent<Rigidbody>().velocity *= 2;
        }
    }
    public void parryDisc()
    {
        int num = UnityEngine.Random.Range(1, 2);
        //play collision sounds
        if (num == 1)
        {
            FindObjectOfType<AudioManager>().Play("Swipe1");
        }
        if (num == 2)
        {
            FindObjectOfType<AudioManager>().Play("Swipe2");
        }   
        diskState = "thrown";
        pmh.playerState = "fieldState";
        diskSpeed = baseDiskSpeed;
        addedThrowVector = pmh.transform.forward;
        rb.velocity = addedThrowVector * diskSpeed * 1.25f;
        resetDiscData();
    }
    public void throwDisc()
    {
        int num = UnityEngine.Random.Range(1, 4);
        //play collision sounds
        if (num == 1)
        {
            FindObjectOfType<AudioManager>().Play("Throw1");
        }
        if (num == 2)
        {
            FindObjectOfType<AudioManager>().Play("Throw2");
        }
        if (num == 3)
        {
            FindObjectOfType<AudioManager>().Play("Throw3");
        }
        if (num == 4)
        {
            FindObjectOfType<AudioManager>().Play("Throw4");
        }
        diskState = "thrown";
        pmh.playerState = "fieldState";
        diskSpeed = baseDiskSpeed * 1 + (5 - pah.timeCaughtElapsed);
        addedThrowVector.Normalize();
        addedThrowVector = pmh.transform.forward;
        rb.velocity = addedThrowVector * diskSpeed;
        resetDiscData();
    }
    public void resetDiscData()
    {
        Invoke("resetGravity", 3.0f);
        pah.buttonPressedElapsed = 0; //resets button pressed time
        pah.timeCaughtElapsed = 0; //Sets time caught elapsed back to 0
        pah.maxTimeCaught = false; //sets max time back to false
        changeYIt = 0;
        initThrowVector = new Vector3(0.0f, 0.0f, 0.0f);
        targetThrowVector = new Vector3(0.0f, 0.0f, 0.0f);
        addedThrowVector = new Vector3(0.0f, 0.0f, 0.0f);
        angling = false;
        pmh = null;
        pah = null;
    }
    public void resetGravity()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        gravityEnabled = false;
    }
    public void setGravityManip()
    {
        gravityManip = true;
        Invoke("resetGravityManip", 7.0f);
    }
    public void resetGravityManip()
    {
        recentGravManip = true;
        Invoke("resetRecentGravManip", 1.0f);
        gravityManip = false;
    }
    public void resetRecentGravManip()
    {
        recentGravManip = false;
    }
    void aimingDirection()
    {
        if (pmh.currentPlayer == playerMovementHandler.player.one)
        {
            if (initThrowVector.y == 0 && initThrowVector.x == 0)
            {
                if (pmh.playerSide == "left")
                {
                    initThrowVector = new Vector3(1.0f, 0.0f, 0.0f);
                }
                else if (pmh.playerSide == "right") 
                {
                    initThrowVector = new Vector3(-1.0f, 0.0f, 0.0f);
                }
            }
            else
            {
                //GetAxis
                //targetThrowVector.y = Input.GetAxis("horizonal");
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    targetThrowVector.y = 1.0f;
                    //addedThrowVector = Vector3.Slerp(initThrowVector, targetThrowVector, slerpSpeed);
                    addedThrowVector = new Vector3(initThrowVector.x,targetThrowVector.y, 0.0f);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    targetThrowVector.x = -1.0f;
                    //````addedThrowVector = Vector3.Slerp(initThrowVector, targetThrowVector, slerpSpeed);
                    addedThrowVector = new Vector3(initThrowVector.x, targetThrowVector.y, 0.0f);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    targetThrowVector.y = -1.0f;
                    //addedThrowVector = Vector3.Slerp(initThrowVector, targetThrowVector, slerpSpeed);
                    addedThrowVector = new Vector3(initThrowVector.x, targetThrowVector.y, 0.0f);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    targetThrowVector.x = 1.0f;
                    addedThrowVector = new Vector3(initThrowVector.x, targetThrowVector.y, 0.0f);
                    //addedThrowVector = Vector3.Slerp(initThrowVector, targetThrowVector, slerpSpeed);
                }
                if (addedThrowVector == Vector3.zero)
                {
                    addedThrowVector = initThrowVector;
                }
            }
        }
        if (pmh.currentPlayer == playerMovementHandler.player.two) 
        {
            if (initThrowVector.y == 0 && initThrowVector.x == 0)
            {
                if (pmh.playerSide == "left")
                {
                    initThrowVector = new Vector3(1.0f, 0.0f, 0.0f);
                }
                else if (pmh.playerSide == "right")
                {
                    initThrowVector = new Vector3(-1.0f, 0.0f, 0.0f);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    targetThrowVector.y = 1.0f;
                    //addedThrowVector = Vector3.Slerp(initThrowVector, targetThrowVector, slerpSpeed);
                    addedThrowVector = new Vector3(initThrowVector.x, targetThrowVector.y, 0.0f);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    targetThrowVector.x = -1.0f;
                    //addedThrowVector = Vector3.Slerp(initThrowVector, targetThrowVector, slerpSpeed);
                    addedThrowVector = new Vector3(initThrowVector.x, targetThrowVector.y, 0.0f);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    targetThrowVector.y = -1.0f;
                    //addedThrowVector = Vector3.Slerp(initThrowVector, targetThrowVector, slerpSpeed);
                    addedThrowVector = new Vector3(initThrowVector.x, targetThrowVector.y, 0.0f);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    targetThrowVector.x = 1.0f;
                    addedThrowVector = new Vector3(initThrowVector.x, targetThrowVector.y, 0.0f);
                    //addedThrowVector = Vector3.Slerp(initThrowVector, targetThrowVector, slerpSpeed);
                }
                if (addedThrowVector == Vector3.zero)
                {
                    Debug.Log("no Y target");
                    addedThrowVector = initThrowVector;
                }
            }
        }    
    }
    public void SHtwoBarUlt()
        {         
                baseDiskSpeed = baseDiskSpeed * 1.025f;
                if(pmh.playerSide == "left")
                {
                    initThrowVector.x = 1.25f;
                }   
                else if(pmh.playerSide == "right")
                {
                    initThrowVector.x = -1.25f;
                }    
                addedThrowVector = new Vector3(initThrowVector.x, initThrowVector.y + pmh.transform.rotation.normalized.y, 0.0f);
                rb.velocity = addedThrowVector * baseDiskSpeed;
        addedThrowVector.Normalize();
        diskState = "thrown";
        pmh.playerState = "fieldState";
        //resetDiscData();
        Invoke("changeY", 0.25f);
        Invoke("changeY", 0.75f);
    }
    public void changeY()
    {
        changeYIt++;
        Instantiate(puckVisage, this.gameObject.transform.position, transform.rotation);
        addedThrowVector.y *= -1;
        rb.velocity = addedThrowVector * baseDiskSpeed;
        Debug.Log("switch!");
        print(addedThrowVector);   
        if (changeYIt >= 2) resetDiscData();
    }
    public void teleChangeY()
    {
        Vector3 tempVel = rb.velocity;
        rb.velocity = new Vector3(tempVel.x, -tempVel.y, tempVel.z);
    }
    public void teleChangeX()
    {
        Vector3 tempVel = rb.velocity;
        rb.velocity = new Vector3(-tempVel.x, tempVel.y, tempVel.z);
    }
    public void bellySlam()
    {
        Debug.Log("Belly Slam");
        diskState = "thrown";
        pmh.playerState = "fieldState";
        if (initThrowVector.y == 0 && initThrowVector.x == 0)
        {
            if (pmh.playerSide == "left")
            {
                initThrowVector = new Vector3(1.0f, 0.0f, 0.0f);
            }
            else if (pmh.playerSide == "right")
            {
                initThrowVector = new Vector3(-1.0f, 0.0f, 0.0f);
            }
        }
        targetThrowVector.y = UnityEngine.Random.Range(-6.0f, 6.0f);
        addedThrowVector = initThrowVector + targetThrowVector;
        addedThrowVector.Normalize();
        rb.velocity = new Vector3(addedThrowVector.x, addedThrowVector.y, 0.0f) * baseDiskSpeed * 3;
        resetDiscData();
    }
    public void garyCrossbow()
    {
        Debug.Log("Crossbow");
        int num = UnityEngine.Random.Range(1, 4);
        //play collision sounds
        if (num == 1)
        {
            FindObjectOfType<AudioManager>().Play("Throw1");
        }
        if (num == 2)
        {
            FindObjectOfType<AudioManager>().Play("Throw2");
        }
        if (num == 3)
        {
            FindObjectOfType<AudioManager>().Play("Throw3");
        }
        if (num == 4)
        {
            FindObjectOfType<AudioManager>().Play("Throw4");
        }
        diskState = "thrown";
        pmh.playerState = "fieldState";
        diskSpeed = baseDiskSpeed * 1 + (5 - pah.timeCaughtElapsed);
        addedThrowVector.Normalize();
        rb.velocity = addedThrowVector * diskSpeed * 1.5f;
        resetDiscData();
    }
    public void theoryOfPower()
    {
        Debug.Log("power!");
        //turn player towards the disk
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-1 * this.gameObject.GetComponent<Rigidbody>().velocity.x, -1 * this.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
    }
    public void addForceAfterCol()
    {
        rb.AddForce(pah.offsetX * 1000.0f, 0, 0);
        resetDiscData();
    }
}