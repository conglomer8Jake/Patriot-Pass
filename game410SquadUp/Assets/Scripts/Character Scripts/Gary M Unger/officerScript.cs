using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class officerScript : MonoBehaviour
{
    public GameObject[] players;
    public GameObject opposingPlayer;

    public float speed;
    void Start()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        Invoke("enableCollider", 0.5f);
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<playerAbilityHandler>().pC != playerAbilityHandler.playerCharacter.garyMUnger)
            {
                opposingPlayer = players[i];
                break;
            }
        }
    }
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, opposingPlayer.transform.position, speed);
        transform.position = Vector3.Slerp(transform.position, opposingPlayer.transform.position, speed * Time.deltaTime);
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<playerAbilityHandler>().pC != playerAbilityHandler.playerCharacter.garyMUnger)
        {
            opposingPlayer.GetComponent<playerMovementHandler>().slowDownPlayer();
            Destroy(this.gameObject);
        }
    }
    public void enableCollider()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}