using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerBuyFreeze : MonoBehaviour
{
    [SerializeField] private Button buybutton;
    [SerializeField] private Button powerbutton;
    [SerializeField] private Image goldIcon;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private float coolOffPeriod =5f;
    [SerializeField] private Powers powers;
    [SerializeField] private int cost;
    private bool isCoolingOff = false;


    private void Awake()
    {
        buybutton = GetComponent<Button>();
        buybutton.onClick.AddListener(BuyButtonClicked);
    }

   

    private void OnEnable()
    {
        UpdateButton();
    }


    private void UpdateButton()
    {
        if (cost <= MoneyManager.Instance.currentGold && !isCoolingOff)
        {
            buybutton.interactable = true;
            goldIcon.enabled = true;
            costText.enabled = true;
        }
        else
        {
            buybutton.interactable = false;
            goldIcon.enabled = false;
            costText.enabled = false;
        }
    }

    private void BuyButtonClicked()
    {
        MoneyManager.Instance.DeductMoney(cost);
        powerbutton.interactable = true;
        isCoolingOff = true;
        Invoke("CooledOff", coolOffPeriod);
        UpdateButton();
    }

    private void CooledOff()
    {
        isCoolingOff = false;
    }
}
