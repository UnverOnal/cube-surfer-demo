using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Movement
{
    private SplineComputer splineComputer;

    private float[] horizontalRange;

    private Transform transformToMove;

    public Movement(SplineComputer splineComputer, float[] horizontalRange, Transform transformToMove)
    {
        this.splineComputer = splineComputer;
        this.horizontalRange = horizontalRange;
        this.transformToMove = transformToMove;
    }

    public void Move(float speed, float sensitivity)
    {
        DreamteckUtility.Move(splineComputer, speed, sensitivity, transformToMove, horizontalRange);
    }
}
