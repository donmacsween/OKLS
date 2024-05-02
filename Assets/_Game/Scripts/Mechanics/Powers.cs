
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Powers : MonoBehaviour
{
    [SerializeField] TowerUpgradeDamageButton damagebutton;
    [SerializeField] TowerUpgradeFireRateButton speedbutton;

    
    [SerializeField] Button Damagebutton;
    [SerializeField] Button Speedbutton;
    [SerializeField] Button Freezebutton;
    [SerializeField] bool DamagePurchased;

       

    private bool freezeOnCoolDown = false;
    private bool damageOnCoolDown = false;
    private bool speedOnCoolDown = false;

    public void BuyFreeze() { Freezebutton.interactable = true; }
    public void BuyDamage() { Damagebutton.interactable = true; }
    public void BuySpeed() { Speedbutton.interactable = true; }

    private void Awake()
    {
        Damagebutton.interactable   = false;
        Speedbutton.interactable    = false;
        Freezebutton.interactable   = false;
    }

    public void CastSlow(InputAction.CallbackContext context)
    {
        if (context.phase.ToString() == "Started")
        {
            Debug.Log("Slow Cast");
            foreach (Enemy enemy in SpawnManager.Instance.activeEnemies)
            {
                enemy.Slow(1.5f, 5f);
            }
            Freezebutton.interactable = false;
        }
    }

    public void DoubleDamage(InputAction.CallbackContext context)
    {
        if (context.phase.ToString() == "Started")
        {
            Debug.Log("Double Damage");
            damagebutton.isDisabled = true;
            foreach (Tower towers in TowerManager.Instance.towers)
            {
                towers.damageMultiplier *= 2f;
            }
            Damagebutton.interactable = false;
            Invoke("NormalDamage", 5f);
        }

    }

    public void DoubleSpeed(InputAction.CallbackContext context)
    {
        if (context.phase.ToString() == "Started")
        {
            Debug.Log("Double Speed");
            speedbutton.isDisabled = true;
            Invoke("NormalSpeed", 5f);
            foreach (Tower towers in TowerManager.Instance.towers)
            {
                towers.fireRate /= 1.5f;
                
            }
        }
    }

    private void UnFreeze() { }
    private void NormalSpeed()
    {
        speedbutton.isDisabled = false;
        foreach (Tower towers in TowerManager.Instance.towers)
        {
            towers.fireRate *= 1.5f;
           
        }
    }

    private void NormalDamage()
    {
        damagebutton.isDisabled = false;
        foreach (Tower tower in TowerManager.Instance.towers)
        {
            tower.damageMultiplier /= 2f;
           
        }
    }
}
