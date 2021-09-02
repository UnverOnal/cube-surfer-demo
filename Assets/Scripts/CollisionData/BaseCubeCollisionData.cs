using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCubeCollisionData : CollisionData
{
    [HideInInspector]public bool isSurfingCube;
    [HideInInspector]public bool hasMultiplierChanged;
    [HideInInspector] public bool isFinish;
    [HideInInspector]public bool isDiamond;

    [HideInInspector]public Transform surfingCubeTransform;
    [HideInInspector]public Transform diamondTransform;

    [HideInInspector]public int currentMultiplier;
    

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        
        switch(other.gameObject.tag)
        {
            case "surfingCube" : 
            isSurfingCube = true;
            surfingCubeTransform = other.transform;
            break;

            case "multiplier" : 
            PlayerManager.instance.currentMultiplierTrasnform = other.transform;
            if(PlayerManager.instance.currentMultiplierTrasnform == PlayerManager.instance.previousMultiplierTransform)
            {
                hasMultiplierChanged = false;
            }
            else
            {
                hasMultiplierChanged = true;
                PlayerManager.instance.previousMultiplierTransform = PlayerManager.instance.currentMultiplierTrasnform;
            }
            currentMultiplier = PlayerManager.instance.currentMultiplierTrasnform.GetComponent<MultiplierData>().Multiplier;
            break;

            case "finishPoint" : 
            isFinish = true;
            break;

            case "diamond" :
            isDiamond = true;
            diamondTransform = other.transform;
            break;
        }
    }
}
