using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ignoreCollisionScript : MonoBehaviour
{
    void Start()
    {
            Physics.IgnoreLayerCollision(6, 7);  
    }
}