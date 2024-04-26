
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerRepairButton : MonoBehaviour
{
    private Tower tower;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text repairCostText;
                     private int      repairCost;
                     private Button   button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(RepairButtonClicked);
    }

    private void OnEnable()
    {
        tower = TowerManager.Instance.GetActiveTower();
        repairCost = tower.GetRepairCost();
        repairCostText.text = repairCost.ToString();
        healthText.text = ((int)tower.health).ToString()+" / " + ((int)tower.maxHealth).ToString();
        
        if (repairCost <= MoneyManager.Instance.currentGold && repairCost > 0)
            {
                button.interactable = true;
            }
        else
            {
                button.interactable = false;
            } 
        
    }
    private void RepairButtonClicked()
    {
        tower.health = tower.maxHealth;
        MoneyManager.Instance.DeductMoney(repairCost);
        healthText.text = tower.health.ToString() + " / " + tower.maxHealth.ToString();
        repairCostText.text = "";
        button.interactable = false;
    }


}
