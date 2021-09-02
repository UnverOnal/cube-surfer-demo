using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackHandler
{
    private Transform stackParent ;
    public List<Transform> stack = new List<Transform>();

    private GameObject dustParticle;

    private Animator animator;

    private ParticleCreator particleCreator;

    public int stackChange;


    public StackHandler(Transform stackParent, Animator animator, ParticleCreator particleCreator)
    {
        this.stackParent = stackParent;
        this.animator = animator;
        this.particleCreator = particleCreator;
    }

    public void AddAll()
    {
        //Adds all children into the stack
        for(int i = 0; i < stackParent.childCount; i++)
        {
            if(stackParent.GetChild(i).GetComponent<CollisionData>())
                stack.Add(stackParent.GetChild(i));
        }
    }

    public void AddToStack(List<Transform> cubesToAdd)
    {
        foreach(Transform cube in cubesToAdd)
        {
            cube.gameObject.tag = "Untagged";
            cube.gameObject.AddComponent<IntervalCubeCollisionData>();

            cube.SetParent(stackParent);

            animator.SetBool("canJump", true);

            //Updating cubes position
            Vector3 newCubePosition = stack[stack.Count - 1].transform.position;
            newCubePosition.y += 1f;
            cube.position = newCubePosition;

            UiManager.instance.pointTextCreator.
            CreatePointText(CameraManager.instance.Camera.WorldToScreenPoint(newCubePosition),
            UiManager.instance.pointTextCreator.transform);

            //Create particle
            particleCreator.CreateParticle(newCubePosition, PoolManager.instance.dustParticlePool);

            stack.Add(cube);

            stackChange++;
        }
    }

    ///<summary>
    ///Can remove multiple cubes.
    ///</summary>
    public void RemoveFromStack(List<Transform> cubesToRemove)
    {
        foreach(Transform cube in cubesToRemove)
        {
            RemoveFromStack(cube);
        }
    }

    ///<summary>
    ///Can remove one cube.
    ///</summary>
    public void RemoveFromStack(Transform cubeToRemove)
    {
        if(stack.Count < 1)
            return;

        cubeToRemove.SetParent(null);

        stack.Remove(cubeToRemove);

        particleCreator.CreateParticle(cubeToRemove.position, PoolManager.instance.dustParticlePool);
        UpdateBottomOfStack();

        stackChange--;
    }

    //Runs after the stack is updated.
    private void UpdateBottomOfStack()
    {
        if(stack.Count < 1)
            return;
            
        MonoBehaviour.Destroy(stack[0].GetComponent<IntervalCubeCollisionData>());
        stack[0].gameObject.AddComponent<BaseCubeCollisionData>();
    }
}
