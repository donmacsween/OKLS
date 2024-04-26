using UnityEngine;
using UnityEngine.UI;

public class TowerDestroyButton : MonoBehaviour
{
    private Button button;
    private Tower tower;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(DestroyButtonClicked);
    }

    private void OnEnable()
    {
        tower = TowerManager.Instance.GetActiveTower();
    }

        private void DestroyButtonClicked()
    {
        tower.PlayerDestroyTower();
        UIManager.Instance.HidePanel();
    }
}
