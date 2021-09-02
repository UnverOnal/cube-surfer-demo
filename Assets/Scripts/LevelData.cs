using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class LevelData : MonoBehaviour
{
    public SplineComputer splineComputer;

    [SerializeField]private Transform multiplierParent;

    private Transform[] multipliers;
    public Transform[] Multipliers 
    { 
        get
        {
            if(multipliers == null)
            {
                for(int i = 0; i < multiplierParent.childCount; i++)
                    multipliers[i] = multiplierParent.GetChild(i);
            }

            return multipliers;
        }
    }
}
