
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Powers : MonoBehaviour
{
    [SerializeField] Button damagebutton;
    [SerializeField] Button speedbutton;

    
    [SerializeField] Button buyDamagebutton;
    [SerializeField] Button buySpeedbutton;
    [SerializeField] Button buyFreezebutton;

    private bool freezeOnCoolDown = false;
    private bool damageOnCoolDown = false;
    private bool speedOnCoolDown = false;
    public void CastSlow(InputAction.CallbackContext context)
    {
        if (context.phase.ToString() == "Started")
        {
            Debug.Log("Slow Cast");
            foreach (Enemy enemy in SpawnManager.Instance.activeEnemies)
            {
                enemy.Slow(1.5f, 5f);
            }
        }
    }

    public void DoubleDamage(InputAction.CallbackContext context)
    {
        if (context.phase.ToString() == "Started")
        {
            Debug.Log("Double Damage");
            foreach (Tower towers in TowerManager.Instance.towers)
            {
                towers.damageMultiplier *= 2f;
                damagebutton.interactable = false;
                Invoke("NormalDamage", 5f);
            }
        }

    }

    public void DoubleSpeed(InputAction.CallbackContext context)
    {
        if (context.phase.ToString() == "Started")
        {
            Debug.Log("Double Speed");
            foreach (Tower towers in TowerManager.Instance.towers)
            {
                towers.fireRate /= 1.5f;
                speedbutton.interactable = false;
                Invoke("NormalSpeed", 5f);
            }
        }
    }

    private void NormalSpeed()
    {
        foreach (Tower towers in TowerManager.Instance.towers)
        {
            towers.fireRate *= 1.5f;
            speedbutton.interactable = true;
        }
    }

    private void NormalDamage()
    {
        foreach (Tower tower in TowerManager.Instance.towers)
        {
            tower.damageMultiplier /= 2f;
            damagebutton.interactable = true;
        }
    }
}
