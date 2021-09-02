using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public void UpdateHeight(int cubeCountToBeAdded)
    {
        Vector3 newCharPosition = transform.position;
        newCharPosition.y += 1f * cubeCountToBeAdded;
        transform.position = newCharPosition;
    }
}
