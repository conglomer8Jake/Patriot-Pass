using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasonPond : MonoBehaviour
{   
    public enum state {Frozen, Freezing, Thawing, Thawed};
    public state currentState;
    public bool isFreezing;
    public float tempature;
    public float thawingTemp; 
    public float stateTime;
    public float baseSize = 15;
    public float scaler = 0.01f;

    private float freezingTemp = 0;
    private float minFreezingTemp;
    private float maxThawingTemp;
    private playerMovementHandler player;
    private GameObject playerObj;
    public float thawedSpeed = 100;
    public float frozenSpeed = 600;
    public float inbetweenSpeed = 300;
    
    private void Start()
    {
        minFreezingTemp = freezingTemp - stateTime;
        maxThawingTemp = thawingTemp + stateTime;
    }

    void Update()
    {
        PondStateControl();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
        {
            if (currentState == state.Frozen)
            {
                playerObj = collisionInfo.gameObject;
                player = playerObj.GetComponent<playerMovementHandler>();
                player.speed = frozenSpeed;
            }
            else if(currentState == state.Thawed)
            {
                playerObj = collisionInfo.gameObject;
                player = playerObj.GetComponent<playerMovementHandler>();
                player.speed = thawedSpeed;
            }
            else
            {
                playerObj = collisionInfo.gameObject;
                player = playerObj.GetComponent<playerMovementHandler>();
                player.speed = inbetweenSpeed;
            }
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
        {
            playerObj = collisionInfo.gameObject;
            player = playerObj.GetComponent<playerMovementHandler>();
            player.resetSpeed();
        }
    }

    private void PondStateControl()
    {
        if (currentState == state.Freezing)
        {
            tempature -= Time.deltaTime;
            baseSize += scaler * Time.deltaTime;
        }
        else if (currentState == state.Thawing)
        {
            tempature += Time.deltaTime;
            baseSize -= scaler * Time.deltaTime;
        }
        else if (currentState == state.Frozen)
        {
            tempature -= Time.deltaTime;
        }
        else if (currentState == state.Thawed)
        {
            tempature += Time.deltaTime;
        }

        if (tempature <= freezingTemp && tempature >= minFreezingTemp && isFreezing)
        {
            currentState = state.Frozen;
        }
        else if (tempature <= minFreezingTemp && isFreezing)
        {
            currentState = state.Thawing;
            isFreezing = false;
        }
        if (tempature >= thawingTemp && tempature <= maxThawingTemp && !isFreezing)
        {
            currentState = state.Thawed;
        }
        else if (tempature >= maxThawingTemp && !isFreezing)
        {
            currentState = state.Freezing;
            isFreezing = true;
        }

        transform.localScale = new Vector3(baseSize, 0.2f, baseSize);

    }
}
