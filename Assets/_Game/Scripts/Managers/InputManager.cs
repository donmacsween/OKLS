using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask towerLayer;
    private Ray         ray;
    private RaycastHit  hit;
    

    // Start is called before the first frame update
    void Awake()
    {
       if (camera == null)
        {
            camera = Camera.main;
        } 
    }

    public void Click (InputAction.CallbackContext context)
    {
        
       
        if  (context.phase.ToString() == "Started")
        {
            ray = camera.ScreenPointToRay(Pointer.current.position.value);
            if (Physics.Raycast(ray, hitInfo: out hit) && hit.collider)
            {
                if (hit.collider.gameObject.layer == 8 && !hit.collider.gameObject.GetComponent<TowerBase>().built)
                //selected  TowerBase
                {
                    Debug.Log("Can build");
                    TowerManager.Instance.SetActiveTowerBase(hit.collider.gameObject.GetComponent<TowerBase>());
                    UIManager.Instance.ShowPanel(UIManager.Instance.TowerPurchasePanel);
                }
                if (hit.collider.gameObject.layer == 9)
                //selected  TowerBase
                {
                    Debug.Log("Selected Tower");
                    TowerManager.Instance.SetActiveTower(hit.collider.gameObject.GetComponent<Tower>());
                    UIManager.Instance.ShowPanel(UIManager.Instance.TowerUpgradePanel);
                }
            }
        }
       
    }

    // drag

    public void Power1()
    {

    }
    public void Power2()
    {

    }
    public void Power3()
    {

    }
    public void Menu()
    {

    }

    public void MouseZoom()
    {

    }



}
