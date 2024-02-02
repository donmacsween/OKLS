using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "GameData/TowerTypes", order = 1)]
public class TowerTypeSO : ScriptableObject
{
    public GameObject towerPrefab;
    public string     towerName;
    public int        towerCost = 50;
    public Sprite     towerIcon;
    public bool       towerUnlocked = true;
}
