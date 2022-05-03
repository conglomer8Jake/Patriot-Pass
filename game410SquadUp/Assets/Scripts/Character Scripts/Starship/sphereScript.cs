using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereScript : MonoBehaviour
{
    public GameObject[] players;
    public GameObject opposingPlayer;

    private float speed;
    void Start()
    {
        speed = Random.Range(1.0f, 2.5f);
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players.Length);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<playerAbilityHandler>().pC != playerAbilityHandler.playerCharacter.starship)
            {
                opposingPlayer = players[i];
                break;
            }
        }
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, opposingPlayer.transform.position, speed * Time.deltaTime);
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<playerAbilityHandler>().pC != playerAbilityHandler.playerCharacter.garyMUnger)
        {
            opposingPlayer.GetComponent<playerMovementHandler>().slowDownPlayer();
            Destroy(this.gameObject);
        }
    }
}