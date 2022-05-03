using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandstormHandler : MonoBehaviour
{
    public Material sand;
    public bool fastDec;
    public float currentT;
    public float disperseSpeed;
    public void Start()
    {
        Invoke("disperseFaster", 5.0f);
        Invoke("destroySand", 6.0f);
    }
    public void Update()
    {
        if (fastDec)
        {
            disperseSpeed = 1.25f;
        } else
        {
            disperseSpeed = 0.025f;
        }
        if (currentT > 0)
        {
            currentT -=  disperseSpeed * Time.deltaTime;
        } else
        {
            currentT = 0;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(sand.color.r, sand.color.g, sand.color.b, currentT);
    }
    public void disperseFaster()
    {
        fastDec = true;
    }
    public void destroySand()
    {
        Destroy(this.gameObject);
    }
}