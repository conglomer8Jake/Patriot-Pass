using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puckVisageHandler : MonoBehaviour
{
    public GameObject puck;
    public DiskMovement dM;
    public Rigidbody rB;
    public Vector3 puckVec;
    void Start()
    {
        puck = GameObject.FindGameObjectWithTag("Puck");
        dM = puck.GetComponent<DiskMovement>();
        rB = GetComponent<Rigidbody>();
        puckVec = new Vector3(dM.addedThrowVector.x, -1 * dM.addedThrowVector.y, 0);
        rB.velocity = puckVec * 10f;
        Invoke("destroyThis", 1.5f);
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            destroyThis();
        }
    }
    public void destroyThis()
    {
        Destroy(this.gameObject);
    }
}
