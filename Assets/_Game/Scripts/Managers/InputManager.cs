using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System.Collections;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance { get; private set; }
    //
    [SerializeField] private Camera                     camera;
    [SerializeField] private LayerMask                  towerLayer;
                     private Ray                        ray;
                     private RaycastHit                 hit;
    // Camera zoom fields
    [SerializeField] private CinemachineVirtualCamera   virtualCamera;
    [SerializeField] private float                      zoomInValue = 50;
    [SerializeField] private float                      zoomOutValue = 200;
    [SerializeField] private float                      zoomSpeed = 3f;
                     private Vector3                    zoomIncrement;
    // Camera panning fields
    [SerializeField] private GameObject                 cameraFocusTarget;
                     private bool                       pointerDown         = false;
                     private Vector2                    lastPosition        = Vector2.zero;
                     private float                      dragSensitivity     = 5f;
                     public  Vector4                    bounds; // Set from the bounds.cs script OnEnable.


    // Start is called before the first frame update
    void Awake()
    {
        // singleton logic
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        // fallback for not setting the camera in the inspector
        if (camera == null) { camera = Camera.main; }
        // fallback for not setting the camera focus target in the inspector
        if (cameraFocusTarget == null)
        {
            GameObject[] taggedObject;
            taggedObject = GameObject.FindGameObjectsWithTag("CameraFocus");
            if(taggedObject.Length > 0 )
            {
                cameraFocusTarget = taggedObject[0];
            }
            else
            {
                Debug.LogError("Camera Focus not set on InputManager");
            }
        }
        zoomIncrement = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.normalized;
    }
        public void Click (InputAction.CallbackContext context)
    {
        pointerDown = true;
        lastPosition = Pointer.current.position.value;
        if (context.phase.ToString() == "Started")
        {
            
            ray = camera.ScreenPointToRay(Pointer.current.position.value);
            if (Physics.Raycast(ray, hitInfo: out hit) && hit.collider)
            {
                Debug.Log(hit.collider.gameObject.layer.ToString());
                if (hit.collider.gameObject.layer == 8 && !hit.collider.gameObject.GetComponent<TowerBase>().built)
                {
                    TowerManager.Instance.SetActiveTowerBase(hit.collider.gameObject.GetComponent<TowerBase>());
                    UIManager.Instance.ShowPanel(UIManager.Instance.TowerPurchasePanel);
                }
                if (hit.collider.gameObject.layer == 9)
                {
                    TowerManager.Instance.SetActiveTower(hit.collider.gameObject.GetComponent<Tower>());
                    UIManager.Instance.ShowPanel(UIManager.Instance.TowerUpgradePanel);
                }
            }
        }

        if (context.phase.ToString() == "Canceled") { pointerDown = false; }
    }

    private void Update()
    {
        if (pointerDown && lastPosition != Pointer.current.position.value) {MoveCameraTarget();}
    }

    private void MoveCameraTarget()
    {
        cameraFocusTarget.transform.position += new Vector3(Pointer.current.delta.value.x, 0, Pointer.current.delta.value.y) * dragSensitivity * Time.deltaTime;
        // restrain to bounds
        if (cameraFocusTarget.transform.position.z > bounds.w) { cameraFocusTarget.transform.position = new Vector3(cameraFocusTarget.transform.position.x,0f,bounds.w); }
        if(cameraFocusTarget.transform.position.z < bounds.y) { cameraFocusTarget.transform.position = new Vector3(cameraFocusTarget.transform.position.x,0f,bounds.y); }
        if(cameraFocusTarget.transform.position.x > bounds.x) { cameraFocusTarget.transform.position = new Vector3(bounds.x,0f, cameraFocusTarget.transform.position.z); }
        if(cameraFocusTarget.transform.position.x < bounds.z) { cameraFocusTarget.transform.position = new Vector3(bounds.z,0f, cameraFocusTarget.transform.position.z); }
    }

    public void MoveCameraTarget(Transform location)
    {
        cameraFocusTarget.transform.position = location.position;
        // restrain to bounds
        if (cameraFocusTarget.transform.position.y > 0) { cameraFocusTarget.transform.position = new Vector3(cameraFocusTarget.transform.position.x, 0f, cameraFocusTarget.transform.position.z);}
        if (cameraFocusTarget.transform.position.z > bounds.w) { cameraFocusTarget.transform.position = new Vector3(location.position.x, 0f, bounds.w); }
        if (cameraFocusTarget.transform.position.z < bounds.y) { cameraFocusTarget.transform.position = new Vector3(location.position.x, 0f, bounds.y); }
        if (cameraFocusTarget.transform.position.x > bounds.x) { cameraFocusTarget.transform.position = new Vector3(bounds.x, 0f, location.position.z); }
        if (cameraFocusTarget.transform.position.x < bounds.z) { cameraFocusTarget.transform.position = new Vector3(bounds.z, 0f, location.position.z); }
    }

    public void Power1()
    {

    }
    public void Power2()
    {

    }
    public void Power3()
    {

    }
    public void ShowPauseMenu()
    {

    }

    public void ZoomIn()
    {
        StartCoroutine(IZoomIn());
    }

    IEnumerator IZoomIn()
    {
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(
                virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset,
                (zoomIncrement * zoomInValue),
                zoomSpeed
                );
        yield return null;
    }

    public void ZoomOut()
    {
        StartCoroutine (IZoomOut());
    }

    IEnumerator IZoomOut()
    {
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(
                virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset,
                (zoomIncrement * zoomOutValue),
                zoomSpeed
                );
        yield return null;
    }


    public void SetDragDensitivity(float newSensitivity)
    {
        if(newSensitivity >1 &&  newSensitivity < 10)
        {
            dragSensitivity = newSensitivity;
        }
    }


}
