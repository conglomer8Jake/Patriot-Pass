using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distortionRenderer : MonoBehaviour
{
    public GameObject Disk;
    void Start()
    {
        Disk = GameObject.FindGameObjectWithTag("Puck");
    }
    void Update()
    {
        if (Disk.GetComponent<DiskMovement>().gravityManip)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        } else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
