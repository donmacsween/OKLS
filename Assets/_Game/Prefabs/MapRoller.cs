using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapRoller : MonoBehaviour
{
    Renderer rend;
    [SerializeField] private float closedRollValue = 0.077f;
    [SerializeField] private float openRollValue   = 1.0f;
    [SerializeField] private float rollSpeed = 1.0f;
                     private float rollValue;
                     private bool  unrolling       = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rollValue = closedRollValue;
        rend.material.shader = Shader.Find("Shader Graphs/MapRollDoubleSidedURP");
    }

    void Update()
    {
        rollValue = rend.material.GetFloat("Vector1_98d33b1d219b486e97f4a6d459a007a3");
        if (rollValue >= closedRollValue && unrolling == false)
        {
            rollValue -= 0.01f * rollSpeed;
            rend.material.SetFloat("Vector1_98d33b1d219b486e97f4a6d459a007a3", rollValue);
        }
        if (rollValue <= openRollValue && unrolling == true)
        {
            rollValue += 0.01f * rollSpeed;
            rend.material.SetFloat("Vector1_98d33b1d219b486e97f4a6d459a007a3", rollValue);
        }



    }

    public void Rollup()
    {
        unrolling = false;
    }
    public void UnRoll()
    {
        unrolling = true;
    }


}