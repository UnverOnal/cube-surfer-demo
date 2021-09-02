using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static InputManager instance;

    [HideInInspector]public PointerEventData eventData;

    private void Awake() 
    {
        //Singleton
        if(instance == null)
            instance = this as InputManager;
        else
            Destroy(gameObject);
    }

    #region Input
    private Vector2 initialPosition;
    private Vector2 deltaPosition;
    [HideInInspector]public Vector2 input;

    //Determines input range on the screen
    [SerializeField] private float maximumDistance = 100f;

    public void OnPointerDown(PointerEventData _eventData)
    {
        eventData = _eventData;
        initialPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        eventData = null;

        //Resets
        deltaPosition = Vector2.zero;
        initialPosition = Vector2.zero;
    }

    private void Update() 
    {
        if(eventData == null) 
            return;

        deltaPosition = eventData.position - initialPosition;
        deltaPosition.x = Mathf.Clamp(deltaPosition.x , -maximumDistance, maximumDistance);
        deltaPosition.y = Mathf.Clamp(deltaPosition.y, -maximumDistance, maximumDistance);

        input = deltaPosition / maximumDistance;
    }
    #endregion

    #region RaycastInput
    // [HideInInspector]public RaycastHit hit;

    // [SerializeField]private Vector2 rayOffset = new Vector2(0f, 100f);

    // [SerializeField]private LayerMask layerMask;

    // [SerializeField]private float maximumRayDistance = 100f;

    // public void OnPointerDown(PointerEventData _eventData)
    // {
    //     eventData = _eventData;
    // }

    // public void OnPointerUp(PointerEventData _eventData)
    // {
    //     eventData = null;

    //     hit = new RaycastHit();
    // }

    // private void FixedUpdate() 
    // {
    //     if(eventData == null)
    //         return;

    //     Ray ray = CameraManager.instance.Camera.ScreenPointToRay(eventData.position + rayOffset);
        
    //     int _layerMask = 1 << layerMask.value;
    //     _layerMask = ~ _layerMask;
    //     Physics.Raycast(ray.origin, ray.direction, out hit, maximumRayDistance, _layerMask);
    // }
    #endregion
}
