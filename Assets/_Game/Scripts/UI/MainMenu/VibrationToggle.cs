using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationToggle : MonoBehaviour
{
    [SerializeField] private GameObject textOn;
    [SerializeField] private GameObject textOff;
    [SerializeField] private GameObject handleOn;
    [SerializeField] private GameObject handleOff;
    private Slider toggle;
    private bool isON;

    private void OnEnable()
    {
        toggle = GetComponent<Slider>();
        ToggleVibrate();
    }

    public void ToggleVibrate ()
    {
        if (toggle.value == 0)
        { 
            isON = true;
        }
        else
        {
            isON=false;
        }

        if (isON)
        {
            textOff.SetActive(true);
            textOn.SetActive(false);
            handleOff.SetActive(true);
            handleOn.SetActive(false);
        }
        else
        {
            textOff.SetActive(false);
            textOn.SetActive(true);
            handleOff.SetActive(false);
            handleOn.SetActive(true);
        }
    }
}
