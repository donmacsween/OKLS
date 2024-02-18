using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    public Transform northBound;
    public Transform southBound;
    public Transform eastBound;
    public Transform westBound;
    public Vector4 bounds;


    private void Awake()
    {
        northBound.gameObject.GetComponent<Renderer>().enabled = false;
        southBound.gameObject.GetComponent<Renderer>().enabled = false;
        eastBound.gameObject.GetComponent<Renderer>().enabled = false;
        westBound.gameObject.GetComponent<Renderer>().enabled = false;
        bounds.w = northBound.position.z;
        bounds.y = southBound.position.z;
        bounds.x = eastBound.position.x;
        bounds.z = westBound.position.x;
        
    }
    private void Start()
    {
        InputManager.Instance.bounds = bounds;
    }

}      
  
