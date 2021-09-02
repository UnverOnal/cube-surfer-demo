using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance ;

    public Pool pointTextPool;
    public Pool dustParticlePool;
    public Pool diamondUiPool;

    private void Awake() 
    {
        if(instance == null)
            instance = this as PoolManager;
        else    
            Destroy(gameObject);     
    }
}
