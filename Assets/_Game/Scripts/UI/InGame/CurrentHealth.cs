using TMPro;
using UnityEngine;

public class CurrentHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text healthLable;

    private void Awake()
    {
        if (healthLable == null) { healthLable = gameObject.GetComponent<TMP_Text>(); }
    }

    void OnEnable()
    {
        BaseManager.OnBaseHealthUpdated += UpdateHealth;
        UpdateHealth();
    }
    void OnDisable()
    {
        BaseManager.OnBaseHealthUpdated -= UpdateHealth;
    }
    private void UpdateHealth()
    {
        if (MoneyManager.Instance != null)
        {
            healthLable.text = BaseManager.Instance.currentHealth.ToString();
        }
    }
}

