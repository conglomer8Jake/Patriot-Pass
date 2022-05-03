using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    private playerMovementHandler player;
    private GameObject playerObj;
    public float puddleSpeed = 100;

    void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Player")
        {
            Debug.Log("has entered");
            playerObj = collisionInfo.gameObject;
            player = playerObj.GetComponent<playerMovementHandler>();
            player.speed = puddleSpeed;
            Debug.Log(player +"has entered");
        }
    }

    void OnTriggerExit(Collider collisionInfo)
    {
        Debug.Log("has exited");
        if (collisionInfo.gameObject.tag == "Player")
        {
            playerObj = collisionInfo.gameObject;
            player = playerObj.GetComponent<playerMovementHandler>();
            player.resetSpeed();
            Debug.Log(player + "has exited");
        }
    }
}
