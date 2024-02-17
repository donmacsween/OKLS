using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "GameData/TowerTypes", order = 1)]
public class TowerTypeSO : ScriptableObject
{
    public GameObject   towerPrefab;
    public string       towerName;
    public int          towerCost            = 50;
    public Sprite       towerIcon;
    public bool         towerUnlocked        = true;

    public float        maxhealth            = 100f;
    public float        health               = 100f;
    public float        fireRate             = 0.7f;
    public float        range                = 10f;
    public float        damageMultiplier     = 2f;
    public float        repairCostMultiplier = 0.5f;
    public AudioClip[]  towerFiringSounds    = null;
}
