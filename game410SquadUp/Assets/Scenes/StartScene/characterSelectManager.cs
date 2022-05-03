using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSelectManager : MonoBehaviour
{
    public Global global;
    public List<string> players;
    void Start()
    {
        players.Clear();
        global.players.Clear();
        CharSelect_new.characterDelegator += characterAssigner;
    }
    public void characterAssigner(int i, string s)
    {
        if (i < 4)
        {
            players.Add(s);
            global.players = players;
        }
    }
}
