using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager  Instance { get; private set; }

                     public TowerBase      activeTowerBase = null;
                     public Tower          activeTower     = null;
    [SerializeField] private GameObject     towerContainer;
                     private GameObject     newTower;
                     public  List<Tower>    towers;

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
        towers = new List<Tower>();
    }
    public void SetActiveTowerBase(TowerBase towerBase)
    {
        activeTowerBase= towerBase;
        Debug.Log("TowerBase Set");
    }
    public void SetActiveTower(Tower tower)
    {
        activeTower = tower;
        Debug.Log("Tower " + tower.gameObject.name +" Set");
    }

    public Tower GetActiveTower()
    {
        return activeTower;
    }
    public void PlaceTower(TowerTypeSO tower)
    {
        newTower = Instantiate(tower.towerPrefab,activeTowerBase.buildPoint.position,activeTowerBase.buildPoint.rotation);
        activeTowerBase.built = true;
        towers.Add(newTower.gameObject.GetComponentInChildren<Tower>());
        UIManager.Instance.HidePanel();
    }

}
