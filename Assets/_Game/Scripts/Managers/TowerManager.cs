using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager  Instance { get; private set; }

    [SerializeField] private TowerBase     activeTowerBase = null;
    [SerializeField] private Tower          activeTower = null;
    [SerializeField] private GameObject    towerContainer;
    private GameObject newTower;

    private void Awake()
    {
        // singleton logic
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void SetActiveTowerBase(TowerBase towerBase)
    {
        activeTowerBase= towerBase;
        Debug.Log("TowerBase Set");
    }
    public void SetActiveTower(Tower tower)
    {
        activeTower = tower;
        Debug.Log("TowerBase Set");
    }
    public void PlaceTower(TowerTypeSO tower)
    {
        newTower = Instantiate(tower.towerPrefab,activeTowerBase.buildPoint.position,activeTowerBase.buildPoint.rotation);
        activeTowerBase.built = true;
        UIManager.Instance.HidePanel(UIManager.Instance.TowerPurchasePanel);
        Debug.Log("Placed");
    }

}
