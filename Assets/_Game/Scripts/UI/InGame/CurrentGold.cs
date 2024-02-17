using UnityEngine;
using TMPro;

public class CurrentGold : MonoBehaviour
{
    [SerializeField] private TMP_Text goldLable;    
    
    private void Awake()
    {
        if (goldLable == null) {goldLable=gameObject.GetComponent<TMP_Text>();} 
    }

    void OnEnable()
    {    
        MoneyManager.OnMoneyUpdated += UpdateGold;
        UpdateGold();
    }
    void OnDisable() 
    {
        MoneyManager.OnMoneyUpdated -= UpdateGold;
    }
    private void UpdateGold() 
    {
        if (MoneyManager.Instance != null)
        {
            goldLable.text = MoneyManager.Instance.currentGold.ToString();
        }
    }
}
