using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class CameraFollow
{
    private Transform transformToFollow;
    private Transform followerCamera;

    public CameraFollow(Transform transformToFollow, Transform followerCamera)
    {
        this.transformToFollow = transformToFollow;
        this.followerCamera = followerCamera;
    }

    ///<summary>
    ///Follows target .
    public void Follow(Vector3 positionOffset, float followSmoothness)
    {
        Vector3 targetPosition = transformToFollow.position + positionOffset;
        followerCamera.position = Vector3.Slerp(followerCamera.position, targetPosition, Time.deltaTime * followSmoothness);
    }

    ///<summary>
    ///Keeps target on the starting place of itself on the screen.
    ///</summary>
    public void Follow(Vector3 positionOffset, Vector2 rotationOffset, float followSmoothness)
    {        
        Vector3 targetPosition = transformToFollow.position + positionOffset;
        followerCamera.position = Vector3.Slerp(followerCamera.position, targetPosition, Time.deltaTime * followSmoothness);

        Vector3 rotationOffset3D = rotationOffset;
        Vector3 positionToLookAt = transformToFollow.position + rotationOffset3D;
        followerCamera.LookAt(positionToLookAt);
    }

    ///<summary>
    /// Rotates camera with target.
    ///</summary>
    public void Follow(Vector3 positionOffset, Vector3 rotationOffset, float followSmoothness, StackHandler stackHandler, float zoomFactor)
    {
        Vector3 followOffset = transformToFollow.right * positionOffset.x + transformToFollow.up * positionOffset.y  + transformToFollow.forward * -positionOffset.z;

        Vector3 targetPosition =  transformToFollow.position  + followOffset;
        followerCamera.position = Vector3.Slerp(followerCamera.position, targetPosition, Time.deltaTime * followSmoothness);

        Quaternion targetRotation = transformToFollow.rotation * Quaternion.Euler(rotationOffset);
        followerCamera.rotation = Quaternion.Slerp(followerCamera.rotation, targetRotation, Time.deltaTime * followSmoothness);

        KeepTargetInView(stackHandler, zoomFactor);
    }

    ///<summary>
    ///Uses Dreamteck.
    ///</summary>
    public void Follow(SplineComputer splineComputer, float speed, Vector3 offset)
    {
        DreamteckUtility.Move(splineComputer, speed, followerCamera, offset);
    }

    private void KeepTargetInView(StackHandler stackHandler, float zoomFactor)
    {
        if(stackHandler.stack.Count < 6)
        {
            stackHandler.stackChange = 0; 
            return;
        }

        Vector3 targetPos = followerCamera.position + followerCamera.forward * -stackHandler.stackChange * zoomFactor;
        targetPos = Vector3.Slerp(followerCamera.position, targetPos, Time.deltaTime * 5f);
        followerCamera.position = targetPos;
    }
}
