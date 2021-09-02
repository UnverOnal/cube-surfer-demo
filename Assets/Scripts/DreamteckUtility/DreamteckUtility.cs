using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class DreamteckUtility
{
    private static float distance = 0f;
    private static float positionOnX = 0f;
    
    ///<summary>
    ///Includes horizontal movement.
    ///</summary>
    public static void Move(SplineComputer splineComputer, float speed, float sensitivity, Transform transformToMove, float[] horizontalRange, Vector3 offset = default(Vector3), float startingPercent = 0.02f)
    {
        if (splineComputer == null || !InputManager.instance) return;

        distance += Time.deltaTime * speed;

        double percent = splineComputer.Travel(startingPercent, distance);
        SplineSample splineSample = splineComputer.Evaluate(percent);

        Vector3 desiredPosition = splineSample.position + offset;
        desiredPosition.y = transformToMove.position.y;

        float targetPosionOnX = InputManager.instance.input.x * sensitivity;
        positionOnX = Mathf.Lerp(positionOnX, targetPosionOnX, Time.deltaTime * 5f);
        positionOnX = Mathf.Clamp(positionOnX, horizontalRange[0], horizontalRange[1]);

        transformToMove.position = desiredPosition + splineSample.right * positionOnX;
        transformToMove.rotation = splineSample.rotation;
    }

    ///<summary>
    ///Moves only along the spline. Doesn't include horizontal movement.
    ///</summary>
    public static void Move(SplineComputer splineComputer, float speed, Transform transformToMove, Vector3 offset = default(Vector3), float startingPercent = 0.02f)
    {
        if (splineComputer == null || !InputManager.instance) return;

        distance += Time.deltaTime * speed;

        double percent = splineComputer.Travel(startingPercent, distance);
        SplineSample splineSample = splineComputer.Evaluate(percent);

        Vector3 desiredPosition = splineSample.position + offset;
        desiredPosition.y = transformToMove.position.y;

        transformToMove.position = desiredPosition;
        transformToMove.rotation = splineSample.rotation;
    }

    public static void ResetMovementProgress()
    {
        distance = 0;
        positionOnX = 0;
    }
}
