using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    #region Follow
    [Header("Follow")]
    [SerializeField] private Vector3 positionOffset;

    [SerializeField] private Vector3 rotationOffset;
    
    [SerializeField] private float followSmoothness; 
    [SerializeField]private float zoomFactor = 2f;

    private CameraFollow cameraFollow;

    private Transform transformToFollow
    {
        get
        {
            if(PlayerManager.instance != null)
                return PlayerManager.instance.transform;
            else
                return null;
        }
    }
    #endregion

    private new Camera camera;
    public Camera Camera
    {
        get
        {
            if(camera != null)
                camera = GetComponent<Camera>();

            return camera;
        }
    }

    private void Awake() 
    {
        if(instance == null)
            instance = this as CameraManager;
        else
            Destroy(gameObject);
    }

    private void Start() 
    {
        camera = GetComponent<Camera>();
    }

    private void LateUpdate() 
    {
        if(transformToFollow == null)
            return;

        //Creates and sets camera follow
        if(cameraFollow == null)
            cameraFollow = new CameraFollow(transformToFollow, camera.transform);

        cameraFollow.Follow(positionOffset, rotationOffset, followSmoothness, PlayerManager.instance.stackHandler, zoomFactor);
    }
}
