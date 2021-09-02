using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    //Instances
    [HideInInspector]public Character character;
    private Movement movement;
    [HideInInspector]public StackHandler stackHandler;
    [HideInInspector]public BaseCubeCollisionData baseCubeCollisionData;
    [SerializeField]private Animator animator;

    [HideInInspector]public Transform currentMultiplierTrasnform;
    [HideInInspector]public Transform previousMultiplierTransform;


    [Header("Movement")]
    public float[] movementRange = new float[2];
    public float speed = 10f;
    public float sensitivity = 1f;

    private void Awake() 
    {
        //Singleton
        if(instance == null)
            instance = this as PlayerManager;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        movement = new Movement(GameManager.instance.levelData.splineComputer, movementRange, this.transform);

        stackHandler = new StackHandler(this.transform, animator, GameManager.instance.ParticleCreator);
        stackHandler.AddAll();

        baseCubeCollisionData = stackHandler.stack[0].GetComponent<BaseCubeCollisionData>();

        character = GetComponentInChildren<Character>();
    }

    void Update()
    {
        if(!GameManager.instance.level.gameStateInfo.isGameOn) return;

        movement.Move(speed, sensitivity);

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("surfingAnimation"))
        {
            animator.SetBool("canSurf", true);
            animator.SetBool("canJump", false);
        }

        #region Success&Fail
        //Success
        if(baseCubeCollisionData.isFinish || (baseCubeCollisionData.hasMultiplierChanged && stackHandler.stack.Count == 0))
        {
            GameManager.instance.level.Success();

            baseCubeCollisionData.isFinish = false;

            animator.SetBool("canCelebrate", true);

            Vector3 confettiPosition = character.transform.position;
            GameManager.instance.ParticleCreator.CreateParticle(confettiPosition, "ConfettiExplosionMix");

            GetComponentInChildren<TrailRenderer>().gameObject.SetActive(false);

            return;
        }//Fail
        else if(stackHandler.stack.Count == 0)
        {
            GameManager.instance.level.Fail();

            animator.SetBool("canFall", true);

            GetComponentInChildren<TrailRenderer>().gameObject.SetActive(false);

            return;
        }
        #endregion

        #region UpdatingStack
        //Adds new cubes into the stack.
        List<Transform> cubesToAdd = new List<Transform>();
        Transform baseCube = stackHandler.stack[0];
        baseCubeCollisionData = baseCube.GetComponent<BaseCubeCollisionData>();
        if(baseCubeCollisionData.isSurfingCube)
        {
            Transform transformHit = baseCubeCollisionData.surfingCubeTransform;
            for(int i = 0; i < transformHit.childCount; i++)
                cubesToAdd.Add(transformHit.GetChild(i));

            baseCubeCollisionData.isSurfingCube = false;

            Destroy(transformHit.gameObject);

            //Updating characters position
            PlayerManager.instance.character.UpdateHeight(cubesToAdd.Count);

            stackHandler.AddToStack(cubesToAdd);
        }

        //Removes cubes when they hit the barriers.
        List<Transform> cubesToRemove = new List<Transform>();
        foreach(Transform _transform in stackHandler.stack)
        {
            CollisionData collisionData = _transform.GetComponent<CollisionData>();
            if(collisionData.isBarrier)
            {
                cubesToRemove.Add(_transform);

                collisionData.isBarrier = false;
            }
        }

        if(cubesToRemove.Count > 0)
            stackHandler.RemoveFromStack(cubesToRemove);

        //Removes a cube when it hits th multiplier. 
        if(baseCubeCollisionData.hasMultiplierChanged)
        {
            stackHandler.RemoveFromStack(baseCube);
        }
        #endregion
    
        //Collect Diomands
        if(baseCubeCollisionData.isDiamond)
        {
            Destroy(baseCubeCollisionData.diamondTransform.gameObject);

            Vector3 dimaondPos = CameraManager.instance.Camera.WorldToScreenPoint(baseCubeCollisionData.diamondTransform.position);
            UiManager.instance.gamePanel.CreateDimondUiEffect(dimaondPos);

            UiManager.instance.gamePanel.UpdateDiamondCount();

            baseCubeCollisionData.isDiamond = false;
            baseCubeCollisionData.diamondTransform = null;
        }
    }
}
