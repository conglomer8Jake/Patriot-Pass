using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarahAnimationHandler : MonoBehaviour
{
    private Animator sarahAnimator;
    public string currentStateCheck;
    public float thrownAimTime;
    public bool isMovingCheck, isJumpingCheck, isThrowingCheck, isUlting2Check, isUlting1Check, isUlting3Check;
    void Start()
    {
        sarahAnimator = this.gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        playerMovementHandler.playerStateUpdateSL += changeState;
        playerMovementHandler.playerMovementUpdateSL += detectMovement;
        playerAbilityHandler.playerThrowUpdate += detectThrow;
        playerAbilityHandler.playerUlt2Update += detectUlt2;
        playerAbilityHandler.playerUlt1Update += detectUlt1;
        playerAbilityHandler.playerUlt3Update += detectUlt3;
    }
    private void OnDisable()
    {
        playerMovementHandler.playerStateUpdateSL -= changeState;
        playerMovementHandler.playerMovementUpdateSL -= detectMovement;
        playerAbilityHandler.playerThrowUpdate -= detectThrow;
        playerAbilityHandler.playerUlt2Update -= detectUlt2;
        playerAbilityHandler.playerUlt1Update -= detectUlt1;
        playerAbilityHandler.playerUlt3Update += detectUlt3;
    }
    void Update()
    {     

    }
    public void changeState(string playerState)
    {
        currentStateCheck = playerState;
        if (playerState == "puckState")
        {
            sarahAnimator.SetBool("hasPuck", true);
        }
        else if (playerState == "fieldState")
        {
            sarahAnimator.SetBool("hasPuck", false);
        }
    }
    public void detectMovement(bool isMoving)
    {
        isMovingCheck = isMoving;
        if (isMoving)
        {
            sarahAnimator.SetBool("isWalking", true);
        }
        else
        {
            sarahAnimator.SetBool("isWalking", false);
        }
    }
    public void detectThrow(bool isThrowing)
    {
        isThrowingCheck = isThrowing;
        if(isThrowing)
        {
            sarahAnimator.SetBool("isThrowing", true);
            Invoke("resetThrow", thrownAimTime);
        }
    }
     public void detectUlt1(bool isUlting1)
    {
       isUlting1Check  = isUlting1;
       if(isUlting1)
       {
           sarahAnimator.SetBool("Ult1", true);
            Invoke("resetUlt1", .75f);
       }
    }
    
    
    public void detectUlt2(bool isUlting2)
    {
       isUlting2Check  = isUlting2;
       if(isUlting2)
       {
           sarahAnimator.SetBool("Ult2", true);
            Invoke("resetUlt2", 0.25f);
       }
    }

     public void detectUlt3(bool isUlting3)
    {
       isUlting3Check  = isUlting3;
       if(isUlting3)
       {
           sarahAnimator.SetBool("Ult3", true);
            Invoke("resetUlt3", .75f);
       }
    }
    public void resetThrow()
    {
        sarahAnimator.SetBool("isThrowing", false);
    }
      public void resetUlt2()
    {
        sarahAnimator.SetBool("Ult2", false);
    }
     public void resetUlt1()
    {
        sarahAnimator.SetBool("Ult1", false);
    }
    public void resetUlt3()
    {
        sarahAnimator.SetBool("Ult3", false);
    }
}
