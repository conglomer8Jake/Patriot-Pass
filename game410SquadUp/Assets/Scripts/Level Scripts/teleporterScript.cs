using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporterScript : MonoBehaviour
{
    public gameManager gM;
    public enum teleHeight
    {
        top, bottom
    }
    public enum teleSide
    {
        left, right
    }
    public teleHeight tH;
    public teleSide tS;
    public GameObject puck;
    public GameObject[] teleporters;
    public List<GameObject> linkableTeleporters;
    public GameObject linkedTeleporter;
    public Material teleActive, teleInactive;
    public bool tpRecent;
    public float changeLinkTimer = 500;
    private float rotationSpeed = 0.25f;
    void Start()
    {
        //Setting enums
        {
            if (transform.position.y < 0)
            {
                tH = teleHeight.bottom;
            }
            else
            {
                tH = teleHeight.top;
            }
            if (transform.position.x < 0)
            {
                tS = teleSide.left;
            }
            else
            {
                tS = teleSide.right;
            }
        }
        gM = GameObject.FindObjectOfType<gameManager>();
        teleporters = GameObject.FindGameObjectsWithTag("teleporter");
        for (int i = 0; i < teleporters.Length; i++)
        {
            if (teleporters[i].transform.position.x != this.gameObject.transform.position.x || teleporters[i].transform.position.y != this.gameObject.transform.position.y)
            {
                linkableTeleporters.Add(teleporters[i]);
            }
        }
        changeLink();
    }
    void Update()
    {
        if (changeLinkTimer >= 0)
        {
            changeLinkTimer -= 1 * Time.deltaTime;
        }
        if (changeLinkTimer % 100 == 0)
        {
            changeLink();
        }
        this.gameObject.transform.Rotate(new Vector3(0.0f, 0.0f, rotationSpeed), Space.Self);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Puck") && !tpRecent && other.gameObject.GetComponent<DiskMovement>().diskState == "thrown")
        {
            tpRecent = true;
            linkedTeleporter.GetComponent<teleporterScript>().tpRecent = true;
            puck = other.gameObject;
            puck.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<MeshRenderer>().material = teleInactive;
            SetTeleportRotationSpeed();
            Invoke("teleportDisk", 1.0f);
            Invoke("resetTpRecent", 2.5f);
        }
    }
    public void changeLink()
    {
        bool hasChanged = false;
        while(!hasChanged)
        {
            int rand = Random.Range(0, 2);
            if (linkableTeleporters[rand] != this.gameObject)
            {
                linkedTeleporter = linkableTeleporters[rand];
                hasChanged = true;
            }
        }
    }
    public void teleportDisk()
    {
        puck.GetComponent<MeshRenderer>().enabled = true;
        puck.transform.position = linkedTeleporter.transform.position;
        Debug.Log("Teleporting to " + linkedTeleporter);
        if (linkedTeleporter.GetComponent<teleporterScript>().tH == this.gameObject.GetComponent<teleporterScript>().tH) puck.GetComponent<DiskMovement>().teleChangeY();
    }
    public void resetTpRecent()
    {
        tpRecent = false;
        linkedTeleporter.GetComponent<teleporterScript>().tpRecent = false;
        this.gameObject.GetComponent<MeshRenderer>().material = teleActive;
    }
    public void SetTeleportRotationSpeed()
    {
        rotationSpeed = 0.75f;
        linkedTeleporter.GetComponent<teleporterScript>().rotationSpeed = 0.75f;
        Invoke("NormalizeRotationSpeed", 1.0f);
    }
    public void NormalizeRotationSpeed()
    {
        rotationSpeed = 0.25f;
        linkedTeleporter.GetComponent<teleporterScript>().rotationSpeed = 0.25f;
    }
}