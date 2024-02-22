using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class TowerPurchaseButton : MonoBehaviour
{
    [SerializeField] private TowerTypeSO type;
    [SerializeField] private Button      button;
    [SerializeField] private Image       icon;
    [SerializeField] private TMP_Text    description;
    [SerializeField] private TMP_Text    costText;
                   
    private void Awake()
    {
        if(type == null)
        {
            Debug.LogError("Tower  type not  set  on this " + gameObject.name + "button");
        }
        else
        {
                icon             = gameObject.GetComponent<Image>();
                button           = gameObject.GetComponent<Button>();
            if (type.towerUnlocked)
            {
                icon.sprite      = type.towerIcon;
                description.text = type.towerName;
                costText.text    = type.towerCost.ToString();
            }
        }
    }
    void OnEnable()
    {
        button.onClick.AddListener(PurchaseButtonClicked);
        MoneyManager.OnMoneyUpdated += ShowHide;
        ShowHide(); 
    }
    void OnDisable()
    {
        MoneyManager.OnMoneyUpdated -= ShowHide;
        button.onClick.RemoveListener(PurchaseButtonClicked);
    }   
    // Only update the UI if the money value changes
    void ShowHide()
    {
        // Only make the button interactable if the player can meet the cost
        if (type.towerCost <= MoneyManager.Instance.currentGold)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
     public void PurchaseButtonClicked()
    {
        // towermanager placeTower
        Debug.Log("Purchased");
        MoneyManager.Instance.DeductMoney(type.towerCost);
        TowerManager.Instance.PlaceTower(type);
    }
}
