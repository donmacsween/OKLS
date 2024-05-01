
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
    [SerializeField] TMP_Text repairCostText;
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
        factor = 20f * (tower.healthUpgradeLevel+1);
        Debug.Log("xfactor: " + factor.ToString());
        float working = tower.towerCost / 100f * factor;
        Debug.Log("working " +(tower.towerCost / 100f * factor).ToString());
        cost = (int)working;
        costText.text = cost.ToString();
        if (cost <= MoneyManager.Instance.currentGold && tower.healthUpgradeLevel <5)
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
        switch (tower.healthUpgradeLevel)
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

    private void HealthUpgradeButtonClicked()
    {
        MoneyManager.Instance.DeductMoney(cost);
        tower.UpgradeHealth();
        UpdateButton();
        repairCostText.text = ((int)tower.health).ToString() + " / " + ((int)tower.maxHealth).ToString();
    }
}
