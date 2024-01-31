using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
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

    public void Click ()
    {        
        ray = camera.ScreenPointToRay(Pointer.current.position.value);
        if(Physics.Raycast(ray, hitInfo: out hit) && hit.collider)
        {    
            // Tower

            // Path

            // anything else



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
