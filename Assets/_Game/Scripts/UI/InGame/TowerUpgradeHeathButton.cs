using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUpgradeHeathButton : MonoBehaviour
{
    private Tower tower;
    [SerializeField] Button button;
    [SerializeField] Image level1;
    [SerializeField] Image level2;
    [SerializeField] Image level3;
    [SerializeField] Image level4;
    [SerializeField] Image goldIcon;
    [SerializeField] TMP_Text costText;
    private int cost;
    private float factor;


    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HealthUpgradeButtonClicked);
    }

    private void OnEnable()
    {
        tower = TowerManager.Instance.GetActiveTower();
        UpdateButton();
    }

    private void UpdateButton()
    {
        factor = 20f * tower.healthUpgradeLevel;
        Debug.Log("factor: " + factor.ToString());
        cost = (int)(tower.towerCost / 100f * factor);
        costText.text = cost.ToString();
        if (cost <= MoneyManager.Instance.currentGold && tower.healthUpgradeLevel <4)
        {
            button.interactable = true;
            goldIcon.enabled = true;
        }
        else
        {
            button.interactable = false;
            goldIcon.enabled = false;
        }

        level1.enabled = false;
        level2.enabled = false;
        level3.enabled = false;
        level4.enabled = false;
        switch (tower.healthUpgradeLevel)
        {
            case 1:
                level1.enabled = true;
                break;
            case 2:
                level1.enabled = true;
                level2.enabled = true;
                break;
            case 3:
                level1.enabled = true;
                level2.enabled = true;
                level3.enabled = true;
                break;
            case 4:
                level1.enabled = true;
                level2.enabled = true;
                level3.enabled = true;
                level4.enabled = true;
                break;
        }
    }

    private void HealthUpgradeButtonClicked()
    {
        tower.UpgradeHealth();
        UpdateButton();
    }
}
