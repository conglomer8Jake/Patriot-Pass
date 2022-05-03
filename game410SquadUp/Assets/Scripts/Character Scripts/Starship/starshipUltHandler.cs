using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starshipUltHandler : MonoBehaviour
{
    public gameManager GM;
    public DiskMovement dM;
    public playerAbilityHandler[] pAH;
    public playerMovementHandler pMHfinal;
    public GameObject starshipPlayer;
    public GameObject puckPrefab;
    public Rigidbody rb;

    public float speed;

    Vector3 movementDirection;
    Vector3 movement;
    Vector3 explosionVector;
    void Start()
    {
        GM = GameObject.FindObjectOfType<gameManager>();
        dM = GameObject.FindObjectOfType<DiskMovement>();
        pAH = GameObject.FindObjectsOfType<playerAbilityHandler>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        for (int i = 0; i < pAH.Length; i++)
        {
            if (pAH[i].GetComponent<playerAbilityHandler>().pC == playerAbilityHandler.playerCharacter.starship)
            {
                starshipPlayer = pAH[i].gameObject;
            }
        }
        pMHfinal = starshipPlayer.GetComponent<playerMovementHandler>();
    }
    public void FixedUpdate()
    {
        speed = 200.0f;
        movementDirection = new Vector3(movement.x, movement.y, 0);
        movementDirection.Normalize();
        rb.velocity = movementDirection * speed * Time.deltaTime;
    }
    void Update()
    {
        if (pMHfinal.currentPlayer == playerMovementHandler.player.one)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        if (pMHfinal.currentPlayer == playerMovementHandler.player.two)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            movement.x = Input.GetAxisRaw("Horizontal1");
            movement.y = Input.GetAxisRaw("Vertical1");
        }
        if (pMHfinal.currentPlayer == playerMovementHandler.player.three)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            movement.x = Input.GetAxisRaw("Horizontal2");
            movement.y = Input.GetAxisRaw("Vertical2");
        }
        if (pMHfinal.currentPlayer == playerMovementHandler.player.four)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            movement.x = Input.GetAxisRaw("Horizontal3");
            movement.y = Input.GetAxisRaw("Vertical3");
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dM.diskState = "thrown";
            pMHfinal.playerState = "fieldState";
            Destroy(this.gameObject);
        }
        {
            //Score left points
            {
                if (other.gameObject.CompareTag("leftNormPoints"))
                {
                    //Normal Goal sound playing
                    FindObjectOfType<AudioManager>().Play("NormalGoalTest");
                    dM.diskState = "thrown";
                    pMHfinal.playerState = "fieldState";
                    Destroy(this.gameObject);
                }
                if (other.gameObject.CompareTag("leftExtraPoints"))
                {
                    //Extra Goal sound playing
                    FindObjectOfType<AudioManager>().Play("ExtraGoalTest");
                    dM.diskState = "thrown";
                    pMHfinal.playerState = "fieldState";
                    Destroy(this.gameObject);
                }
            }
            //Score right points
            {
                if (other.gameObject.CompareTag("rightNormPoints"))
                {
                    //Normal Goal sound playing
                    FindObjectOfType<AudioManager>().Play("NormalGoalTest");
                    dM.diskState = "thrown";
                    pMHfinal.playerState = "fieldState";
                    Destroy(this.gameObject);
                }
                if (other.gameObject.CompareTag("rightExtraPoints"))
                {
                    //Extra Goal sound playing
                    FindObjectOfType<AudioManager>().Play("ExtraGoalTest");
                    dM.diskState = "thrown";
                    pMHfinal.playerState = "fieldState";
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
