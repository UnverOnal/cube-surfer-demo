using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionData : MonoBehaviour
{
    [HideInInspector]public bool isBarrier;

    protected virtual void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("barrier"))
            isBarrier = true;
    }

    protected virtual void OnCollisionExit(Collision other) 
    {
        
    }
}
