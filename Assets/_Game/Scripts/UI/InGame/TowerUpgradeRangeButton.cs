using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TowerUpgradeRangeButton : MonoBehaviour
{
    private Tower tower;
    [SerializeField] private Button button;
    [SerializeField] private Image level1;
    [SerializeField] private Image level2;
    [SerializeField] private Image level3;
    [SerializeField] private Image level4;
    [SerializeField] private Image goldIcon;
    [SerializeField] private TMP_Text costText;
    private int cost;
    private float factor;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(RangeUpgradeButtonClicked);
    }

    private void OnEnable()
    {
        tower = TowerManager.Instance.GetActiveTower();
        UpdateButton();
    }

    private void UpdateButton()
    {
        factor = 20f * tower.rangeUpgradeLevel;
        Debug.Log("factor: " + factor.ToString());
        cost = (int)((tower.towerCost / 100f) * factor);
        costText.text = cost.ToString();
        if (cost <= MoneyManager.Instance.currentGold && tower.rangeUpgradeLevel < 5)
        {
            button.interactable = true;
            goldIcon.enabled = true;
            costText.enabled = true;
        }
        else
        {
            button.interactable = false;
            goldIcon.enabled = false;
            costText.enabled = false;
        }

        level1.enabled = false;
        level2.enabled = false;
        level3.enabled = false;
        level4.enabled = false;
        switch (tower.rangeUpgradeLevel)
        {
            case 2:
                level1.enabled = true;
                break;
            case 3:
                level1.enabled = true;
                level2.enabled = true;
                break;
            case 4:
                level1.enabled = true;
                level2.enabled = true;
                level3.enabled = true;
                break;
            case 5:
                level1.enabled = true;
                level2.enabled = true;
                level3.enabled = true;
                level4.enabled = true;
                break;
        }
    }

    private void RangeUpgradeButtonClicked()
    {
        MoneyManager.Instance.DeductMoney(cost);
        tower.UpgradeRange();
        UpdateButton();
    }
}
