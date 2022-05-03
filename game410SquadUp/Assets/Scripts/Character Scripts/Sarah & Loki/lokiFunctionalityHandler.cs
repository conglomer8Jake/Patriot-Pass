using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lokiFunctionalityHandler : MonoBehaviour
{
    public enum currentPosition
    {
        lRPa, lRPb, lRPc
    }
    public currentPosition cP;
    public GameObject[] players;
    public GameObject opposingPlayer;
    public GameObject sarahPlayer;
    public GameObject lRPa, lRPb, lRPc;
    public Vector3 towardsVec, avoidVec, finalVec;
    public float finalVecMod;
    public string lokiState;
    public float slerpSpeed;
    public float avoidRatio;
    private float towardsRatio;
    public int randomSpot;
    public bool playerAssign = false;
    void Start()
    {
        towardsRatio = 1.0f - avoidRatio;
        slerpSpeed = 0.75f;
        lokiState = "passive";
        players = GameObject.FindGameObjectsWithTag("Player");
        Invoke("randomGen", 0.5f);
    }
    void Update()
    {
        if (sarahPlayer != null && !playerAssign)
        {
            assignRandomPositions();
        }
        if (lokiState == "passive")
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            if (randomSpot <= 2)
            {
                //transform.position = Vector3.Slerp(this.gameObject.transform.position, lRPa.transform.position, slerpSpeed * Time.deltaTime);  
                    towardsVec = lRPa.transform.position - this.gameObject.transform.position;
                    towardsVec.Normalize();
                    avoidVec = this.gameObject.transform.position - sarahPlayer.transform.position;
                    avoidVec.Normalize();
                    finalVec = towardsVec * towardsRatio + avoidVec * avoidRatio;
                    finalVec.Normalize();
                if ((lRPa.transform.position - transform.position).magnitude <= 0.1)
                {
                    this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                else
                {
                    transform.position += (lRPa.transform.position + finalVec * 1 - transform.position) * finalVecMod * Time.deltaTime;
                }
            } else if (randomSpot >= 2 && randomSpot <= 4)
            {
                //transform.position = Vector3.Slerp(this.gameObject.transform.position, lRPb.transform.position, slerpSpeed * Time.deltaTime);
                    towardsVec = lRPb.transform.position - this.gameObject.transform.position;
                    towardsVec.Normalize();
                    avoidVec = this.gameObject.transform.position - sarahPlayer.transform.position;
                    avoidVec.Normalize();
                    finalVec = towardsVec * towardsRatio + avoidVec * avoidRatio;
                    finalVec.Normalize();
                if ((lRPb.transform.position - transform.position).magnitude <= 0.1)
                {
                    this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                } else
                {
                    transform.position += (lRPb.transform.position + finalVec * 1 - transform.position) * finalVecMod * Time.deltaTime;
                }

            } else if (randomSpot >= 4 && randomSpot <= 6)
            {
                //transform.position = Vector3.Slerp(this.gameObject.transform.position, lRPc.transform.position, slerpSpeed * Time.deltaTime);
                    towardsVec = lRPc.transform.position - this.gameObject.transform.position;
                    towardsVec.Normalize();
                    avoidVec = this.gameObject.transform.position - sarahPlayer.transform.position;
                    avoidVec.Normalize();
                    finalVec = towardsVec * towardsRatio + avoidVec * avoidRatio;
                    finalVec.Normalize();
                if ((lRPc.transform.position - transform.position).magnitude <= 0.1)
                {
                    this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                else
                {
                    transform.position += (lRPc.transform.position + finalVec * 1 - transform.position) * finalVecMod * Time.deltaTime;
                }
            }
        }
        if (lokiState == "aggressive")
        {
            transform.position = Vector3.Lerp(this.gameObject.transform.position, opposingPlayer.transform.position, slerpSpeed * Time.deltaTime * 2);
            //transform.position = Vector3.MoveTowards(this.gameObject.transform.position, lRPa.transform.position, slerpSpeed * Time.deltaTime);
            Invoke("enableCollider", 0.5f);
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<playerAbilityHandler>().pC != playerAbilityHandler.playerCharacter.sarahLoki)
        {
            Debug.Log("player collision");
            other.gameObject.GetComponent<playerMovementHandler>().freezePlayer();
            resetLokiState();
        }
    }
    public void assignRandomPositions()
    {
        Debug.Log("assigningPos");
        foreach (Transform child in sarahPlayer.transform)
        {
            if (child.gameObject.CompareTag("lokiPosA"))
            {
                lRPa = child.gameObject;
            }
            if (child.gameObject.CompareTag("lokiPosB"))
            {
                lRPb = child.gameObject;
            }
            if (child.gameObject.CompareTag("lokiPosC"))
            {
                lRPc = child.gameObject;
            }
        }
        playerAssign = true;
    }
    public void resetLokiState()
    {
        lokiState = "passive";
        randomGen();
    }
    public void randomGen()
    {
        randomSpot = Random.Range(0, 6);
        Invoke("randomGen", 2.0f);
    }
    public void enableCollider()
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
}
