using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class superMeter : MonoBehaviour
{
    public Slider superBar;
    public GameObject player;
    public GameObject[] players;
    public enum sideState
    {
        left, right
    }
    public sideState sS;
    void Start()
    {
        if (this.gameObject.transform.localPosition.x < 0)
        {
            sS = sideState.left;
        }
        else
        {
            sS = sideState.right;
        }
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("players.size = " + players.Length);
        superBar.value = 0;
        for (int i = 0; i < players.Length; i++)
        {
            Debug.Log("Checking player[" + i + "]");
            if (players[i].transform.position.x < 0 && sS == sideState.left)
            {
                player = players[i];
                Debug.Log("left player");
            }
            else if (players[i].transform.position.x > 0 && sS == sideState.right)
            {
                player = players[i];
                Debug.Log("right player");
            }
            else
            {
                Debug.Log("!!!!!");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        superBar.value = player.GetComponent<playerAbilityHandler>().ultBar;
    }
}