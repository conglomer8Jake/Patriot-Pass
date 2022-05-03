using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    public GameObject puck;
    public Vector2 puckPos;
    void Start()
    {
        puck = GameObject.FindGameObjectWithTag("Puck");
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Puck"))
        {
            int num = Random.Range(1, 4);
            //play collision sounds
            if(num ==  1)
            {
                FindObjectOfType<AudioManager>().Play("Collision1");
            }
            if (num == 2)
            {
                FindObjectOfType<AudioManager>().Play("Collision2");
            }
            if (num == 3)
            {
                FindObjectOfType<AudioManager>().Play("Collision3");
            }
            if (num == 4)
            {
                FindObjectOfType<AudioManager>().Play("Collision4");
            }
            /*puckPos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y,0);
            this.gameObject.transform.position = new Vector3(puckPos.x, this.gameObject.transform.position.y, 0);*/
            //play animation
        }
    }
}
