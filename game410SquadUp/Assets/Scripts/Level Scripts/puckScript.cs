using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class puckScript : MonoBehaviour
{
    public static event Action score;
    public DiskMovement dM;
    public gameManager GM;
    public Rigidbody rB;
    public float timeCaught;
    public float randomSpeedMin, randomSpeedMax;
    public TextMeshProUGUI scoreLeft, scoreRight;
    void Start()
    {
        dM = this.gameObject.GetComponent<DiskMovement>();
        GM = GameObject.FindObjectOfType<gameManager>();
        rB = this.gameObject.GetComponent<Rigidbody>();
        Invoke("RandomizeStartSpeed", 2.5f);
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        //Score left points
        {
            if (other.gameObject.CompareTag("leftNormPoints"))
            {
                //Normal Goal sound playing
                FindObjectOfType<AudioManager>().Play("LesserGoalScore");

                GM.rightScore += 3;
                UpdatePoints();
                Reposition(this.gameObject);
                score?.Invoke();
            }
            if (other.gameObject.CompareTag("leftExtraPoints"))
            {
                //Extra Goal sound playing
                FindObjectOfType<AudioManager>().Play("GoalScore");

                GM.rightScore += 5;
                UpdatePoints();
                Reposition(this.gameObject);
                score?.Invoke();
            }
        }
        //Score right points
        {
            if (other.gameObject.CompareTag("rightNormPoints"))
            {
                //Normal Goal sound playing
                FindObjectOfType<AudioManager>().Play("LesserGoalScore");

                GM.leftScore += 3;
                UpdatePoints();
                Reposition(this.gameObject);
                score?.Invoke();
            }
            if (other.gameObject.CompareTag("rightExtraPoints"))
            {
                //Extra Goal sound playing
                FindObjectOfType<AudioManager>().Play("GoalScore");

                GM.leftScore += 5;
                UpdatePoints();
                Reposition(this.gameObject);
                score?.Invoke();
            }
        }
        //Boundaries
        if (other.gameObject.CompareTag("bounds"))
        {
            Reposition(this.gameObject);
        }
    }

    void UpdatePoints()
    {
        scoreLeft.text = GM.leftScore.ToString();
        scoreRight.text = GM.rightScore.ToString();
    }

    private void Reposition(GameObject puck) //return puck to center and launch it
    {
        //Play puck   reset  jingle
        FindObjectOfType<AudioManager>().Play("DiskRespawn");

        rB.velocity = RandomizeStartSpeed();
        puck.transform.position = new Vector3(0, 0, 0.79f);
    }
    private Vector3 RandomizeStartSpeed() //sends puck in random direction, some are unfavorable, I can limit the angle range if needed
    {
        
        float ySpeed = UnityEngine.Random.Range(randomSpeedMin, randomSpeedMax); //the speed will always be 10 total
        float xSpeed = 10.0f - Mathf.Abs(ySpeed);
        if (UnityEngine.Random.Range(-1.0f, 1.0f) < 0)
        {
            xSpeed *= -1.0f;
        }
        Vector3 speed = new Vector3(xSpeed, ySpeed, 0);
        rB.velocity = speed;
        return speed;
    }
}