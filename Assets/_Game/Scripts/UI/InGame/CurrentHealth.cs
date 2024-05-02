using UnityEngine;
using TMPro;


public class CurrentHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text healthLable;
    private void Awake() {if (healthLable == null) { healthLable = gameObject.GetComponent<TMP_Text>(); }}
    void OnEnable() {BaseManager.OnBaseHealthUpdated += UpdateHealth;}
    void OnDisable() {BaseManager.OnBaseHealthUpdated -= UpdateHealth;}
    private void Start() {UpdateHealth();}
    private void UpdateHealth()
    { 
        if (MoneyManager.Instance != null)
        {
            if (BaseManager.Instance.currentHealth >= 0)
            {
                healthLable.text = BaseManager.Instance.currentHealth.ToString();
            }
            else
            {
                healthLable.text = "0";
            }
        }
    }
}

