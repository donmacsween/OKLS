using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Enemy", menuName = "GameData/Wave", order = 1)]
public class EnemySO : ScriptableObject
{
    public string       enemyType;
    public GameObject   enemyPrefab;
    public float        baseHealth;
    public float        baseSpeed;
    public float        baseArmour;
    public int          baseMoney;
    public int          baseCastleDamage;
}
