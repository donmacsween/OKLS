
using UnityEngine;
using UnityEngine.UI;

public class Powers : MonoBehaviour
{
    [SerializeField] Button damagebutton;
    [SerializeField] Button speedbutton;
    public void CastSlow()
    {
        foreach (Enemy enemy in SpawnManager.Instance.activeEnemies)
        {
            enemy.Slow(1.5f,5f);
        }
    }

    public void DoubleDamage()
    {
        foreach (Tower towers in TowerManager.Instance.towers)
        {
            towers.damageMultiplier *= 2f;
            damagebutton.interactable = false;
            Invoke("NormalDamage", 5f);
        }

    }

    public void DoubleSpeed()
    {
        foreach (Tower towers in TowerManager.Instance.towers)
        {
            towers.fireRate *= 2f;
            speedbutton.interactable = false;
            Invoke("NormalSpeed", 5f);
        }
    }

    private void NormalSpeed()
    {
        foreach (Tower towers in TowerManager.Instance.towers)
        {
            towers.fireRate /= 2f;
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
