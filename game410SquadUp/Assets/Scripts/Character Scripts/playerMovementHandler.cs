using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class playerMovementHandler : MonoBehaviour
{
    public static event Action<string> playerAssignment;

    public static event Action<bool> playerMovementUpdateRB;
    public static event Action<string> playerStateUpdateRB;
    public static event Action<bool> playerMovementUpdateGU;
    public static event Action<string> playerStateUpdateGU;
    public static event Action<bool> playerMovementUpdateSS;
    public static event Action<string> playerStateUpdateSS;

    public static event Action<bool> playerMovementUpdateSL;
    public static event Action<string> playerStateUpdateSL;
    public enum player 
    { 
    one, two, three, four
    }
    public player currentPlayer;
    public enum team
    {
        one, two
    }
    public team Team;
    public gameManager GM;
    public float speed;
    public float dashSpeed = 1.0f;
    public bool canDash = true;
    public Rigidbody rb;
    public bool isDashing = false;
    public bool isMoving = false;
    public Transform collisionVector;
    public Rigidbody rB;
    public playerAbilityHandler pah;
    public string playerState;
    public string playerSide;
    public DiskMovement dm;
    float turnSmoothVelocity;
    public float charSpeedMod = 1.0f;
    public float turnSmoothTime = 0.1f;
    public float baseSpeed = 500.0f; 
    public float kbMod;
    private Vector3 vectorToStarship;
    public Vector3 movementVector;
    public Vector3 playerForward;
    public float playerRotateSpeed = 2.0f;
    public bool hasPuck;

    public bool fieldState;

    public bool isThrowing;
    
    
    
    //string State;
    //string[] playerStates = {"Field","Puck","Menu"};

    public Vector3 movementDirection;
    public Vector3 movement;
    public Vector3 rotationCall;

    private Animator starshipController;

    void Awake() 
    {
     starshipController = GetComponent<Animator>();
    }   
    void Start()
    {
        resetSpeed();
        rb = this.GetComponent<Rigidbody>();
        GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<gameManager>();
        playerState = "fieldState";
        dm = GameObject.FindGameObjectWithTag("Puck").GetComponent<DiskMovement>();
        rB = this.gameObject.GetComponent<Rigidbody>();
        pah = this.gameObject.GetComponent<playerAbilityHandler>();
        if (transform.position.x < 0)
        {
            playerSide = "left";
            playerAssignment?.Invoke(playerSide);
        } else
        {
            playerSide = "right";
            playerAssignment?.Invoke(playerSide);
        }
        if (pah.pC == playerAbilityHandler.playerCharacter.sarahLoki) charSpeedMod = 1.15f;
        if (pah.pC == playerAbilityHandler.playerCharacter.garyMUnger) charSpeedMod = .75f;
        if (pah.pC == playerAbilityHandler.playerCharacter.Dieterich) charSpeedMod = 1.25f;
    }
    private void FixedUpdate()
    {
        //Debug.Log(transform.forward);
        //sees if the player presses left shift and if you can dash
        if (playerState == "fieldState")
        {
            if (pah.pC == playerAbilityHandler.playerCharacter.starship) playerStateUpdateSS?.Invoke("fieldState");
            if (pah.pC == playerAbilityHandler.playerCharacter.sarahLoki) playerStateUpdateSL?.Invoke("fieldState");
            if (pah.pC == playerAbilityHandler.playerCharacter.garyMUnger) playerStateUpdateGU?.Invoke("fieldState");
            if (pah.pC == playerAbilityHandler.playerCharacter.Dieterich) playerStateUpdateRB?.Invoke("fieldState");
            movementDirection = new Vector3(movement.x, movement.y, movement.z);
            movementDirection.Normalize();
            rb.velocity = movementDirection * speed * dashSpeed * Time.deltaTime * charSpeedMod;
            fieldState = true;
            hasPuck = false;
            movingCheck();         
        }
        else if (playerState == "menuState")
        {

        }
        else if (playerState == "puckState")
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            if (pah.pC == playerAbilityHandler.playerCharacter.starship) playerStateUpdateSS?.Invoke("puckState");
            if (pah.pC == playerAbilityHandler.playerCharacter.sarahLoki) playerStateUpdateSL?.Invoke("puckState");
            if (pah.pC == playerAbilityHandler.playerCharacter.garyMUnger) playerStateUpdateGU?.Invoke("puckState");
            if (pah.pC == playerAbilityHandler.playerCharacter.Dieterich) playerStateUpdateRB?.Invoke("puckState");
            movementDirection = new Vector3(0.0f, 0.0f, movement.z);
            rb.velocity = movementDirection * speed * dashSpeed * Time.deltaTime;    
        }
        else if (playerState == "stunned")
        {

        }
    }
    void Update()
    { 
        if (playerState == "fieldState")
        {
            playerForward = transform.forward;
            rb.constraints = RigidbodyConstraints.FreezeRotation;       
            //Input checking code
            { 
                if (GetButtonThrowDown() && canDash)
                {
                    startDashing();
                    //plays a sound to give audio feedback to dash
                    //FindObjectOfType<AudioManager>().Play("DashTest");
                    Invoke("resetDash", 0.25f);
                    Invoke("canDashAgain", 1.25f);
                    
                }
                {
                    movement.x = GetRawHorizontal();
                    movement.y = GetRawVertical();
                    movementVector = new Vector3(movement.x, movement.y, 0.0f);
                    if (Vector3.Dot(playerForward.normalized, movementVector.normalized) < 0.7 && movementVector != Vector3.zero && playerSide == "left")
                    {
                        if (movement.y > 0)
                        {
                            transform.Rotate(new Vector3(0.0f, -playerRotateSpeed, 0.0f), Space.Self);
                        } else if (movement.y < 0)
                        {
                            transform.Rotate(new Vector3(0.0f, playerRotateSpeed, 0.0f), Space.Self);
                        }
                    }
                    else if (Vector3.Dot(playerForward.normalized, movementVector.normalized) < 0.7 && movementVector != Vector3.zero && playerSide == "right")
                    {
                        if (movement.y > 0)
                        {
                            transform.Rotate(new Vector3(0.0f, playerRotateSpeed, 0.0f), Space.Self);
                        }
                        else if (movement.y < 0)
                        {
                            transform.Rotate(new Vector3(0.0f, -playerRotateSpeed, 0.0f), Space.Self);
                        }
                    }
                }
            }
        }
        else if (playerState == "menuState")
        {
            movingCheck();
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (playerState == "puckState")
        {
            movementDirection = new Vector3(0.0f, 0.0f, movement.z);
            movingCheck();
            rotateCharacter();
        }
    }   
    public void slowDownPlayer()
    {
        speed = baseSpeed * 0.5f;
        Invoke("resetSpeed", 1.5f);
    }
    public void speedUpPlayer()
    {
        speed = baseSpeed * 1.5f;
        Invoke("resetSpeed", 1.5f);
    }
    public void freezePlayer()
    {
        Debug.Log("it cold");
        speed *= 0;
        Invoke("resetSpeed", 3.0f);
    }
    public void startDashing()
    {
        isDashing = true;
        dashSpeed = 3.0f;
        canDash = false;
    }
    public void resetDash()
    {
        isDashing = false;
        dashSpeed = 1f;
    }
    public void resetSpeed()
    {
        speed = baseSpeed;
    }
    public void canDashAgain()
    {
        isDashing = false;
        canDash = true;
    }
    void movingCheck()
    {
        if (pah.pC == playerAbilityHandler.playerCharacter.starship)
        {
            if (GetRawHorizontal() != 0.0f || GetRawVertical() != 0.0f) 
            {
                isMoving = true;
                playerMovementUpdateSS?.Invoke(true);
            }
            else
            {
                isMoving = false;
                playerMovementUpdateSS?.Invoke(false);
            }
        }
        if (pah.pC == playerAbilityHandler.playerCharacter.sarahLoki)
        {
            if (GetRawHorizontal() != 0.0f || GetRawVertical() != 0.0f)
            {
                isMoving = true;
                if (isMoving == true) 
                {
                    playerMovementUpdateSL?.Invoke(true);
                }
            }
            else
            {
                isMoving = false;
                playerMovementUpdateSL?.Invoke(false);
            }
        }
         if (pah.pC == playerAbilityHandler.playerCharacter.garyMUnger)
        {
            if (GetRawHorizontal() != 0.0f || GetRawVertical() != 0.0f) 
            {
                isMoving = true;
                playerMovementUpdateGU?.Invoke(true);
            }
            else
            {
                isMoving = false;
                playerMovementUpdateGU?.Invoke(false);
            }
        }
         if (pah.pC == playerAbilityHandler.playerCharacter.Dieterich)
        {
            if (GetRawHorizontal() != 0.0f || GetRawVertical() != 0.0f) 
            {
                isMoving = true;
                playerMovementUpdateRB?.Invoke(true);
            }
            else
            {
                isMoving = false;
                playerMovementUpdateRB?.Invoke(false);
            }
        }
    }
    public void resetState()
    {
        playerState = "fieldState";
    }
    public void rotateCharacter()
    {
        if (currentPlayer == player.one)
        {
            rotationCall.y = Input.GetAxisRaw("Vertical");
            if (playerSide == "left")
            {
                if (rotationCall.y > 0)
                {
                    transform.Rotate(new Vector3(0.0f, -playerRotateSpeed, 0.0f), Space.Self);
                }
                else if (rotationCall.y < 0)
                {
                    transform.Rotate(new Vector3(0.0f, playerRotateSpeed, 0.0f), Space.Self);
                }
            }
            else if (playerSide == "right")
            {
                if (rotationCall.y > 0)
                {
                    transform.Rotate(new Vector3(0.0f, playerRotateSpeed, 0.0f), Space.Self);
                }
                else if (rotationCall.y < 0)
                {
                    transform.Rotate(new Vector3(0.0f, -playerRotateSpeed, 0.0f), Space.Self);
                }
            }
        } else if (currentPlayer == player.two)
        {
            rotationCall.y = Input.GetAxisRaw("Vertical1");
            if (playerSide == "left")
            {
                if (rotationCall.y > 0)
                {
                    transform.Rotate(new Vector3(0.0f, -playerRotateSpeed, 0.0f), Space.Self);
                }
                else if (rotationCall.y < 0)
                {
                    transform.Rotate(new Vector3(0.0f, playerRotateSpeed, 0.0f), Space.Self);
                }
            }
            else if (playerSide == "right")
            {
                if (rotationCall.y > 0)
                {
                    transform.Rotate(new Vector3(0.0f, playerRotateSpeed, 0.0f), Space.Self);
                }
                else if (rotationCall.y < 0)
                {
                    transform.Rotate(new Vector3(0.0f, -playerRotateSpeed, 0.0f), Space.Self);
                }
            }
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("starshipUlt"))
        {
                vectorToStarship = transform.position - other.transform.position;
                vectorToStarship.Normalize();
                Debug.Log(vectorToStarship);
                rb.AddForce(vectorToStarship * kbMod);
            playerState = "stunned";
            Invoke("resetState", 1.5f);
        }
    }
    public bool GetButtonUltDown()
    {
        if (currentPlayer == player.one)
        {
            return Input.GetKeyDown(KeyCode.Period);
        }
        if (currentPlayer == player.two)
        {
            return Input.GetKeyDown(KeyCode.BackQuote);
        }
        if (currentPlayer == player.three)
        {
            return Input.GetKeyDown(KeyCode.G);
        }
        if (currentPlayer == player.four)
        {
            return Input.GetKeyDown(KeyCode.Keypad1);
        }
        return false;
    }

    public bool GetButtonThrowDown()
    {
        if (currentPlayer == player.one)
        {
            return Input.GetKeyDown(KeyCode.Slash);
        }
        if (currentPlayer == player.two)
        {
            return Input.GetKeyDown(KeyCode.Alpha1);
        }
        if (currentPlayer == player.three)
        {
            return Input.GetKeyDown(KeyCode.H);
        }
        if (currentPlayer == player.four)
        {
            return Input.GetKeyDown(KeyCode.Keypad2);
        }
        return false;
    }

    public float GetRawHorizontal()
    {
        if (currentPlayer == player.one)
        {
            return Input.GetAxisRaw("Horizontal");
        }
        if (currentPlayer == player.two)
        {
            return Input.GetAxisRaw("Horizontal1");
        }
        if (currentPlayer == player.three)
        {
            return Input.GetAxisRaw("Horizontal2");
        }
        if (currentPlayer == player.four)
        {
            return Input.GetAxisRaw("Horizontal3");
        }
        return 0.0f;
    }

    public float GetRawVertical()
    {
        if (currentPlayer == player.one)
        {
            return Input.GetAxisRaw("Vertical");
        }
        if (currentPlayer == player.two)
        {
            return Input.GetAxisRaw("Vertical1");
        }
        if (currentPlayer == player.three)
        {
            return Input.GetAxisRaw("Vertical2");
        }
        if (currentPlayer == player.four)
        {
            return Input.GetAxisRaw("Vertical3");
        }
        return 0.0f;
    }
}