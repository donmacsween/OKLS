
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "GameData/Enemy")]
public class EnemySO : ScriptableObject
{
    public string       enemyType           ="";
    public GameObject   enemyPrefab         = null;
    public float        baseHealth          = 10f;
    public float        baseSpeed           = 1f;
    public float        baseArmour          = 0.2f;
    public int          baseMoney           = 1;
    public int          baseCastleDamage    = 5;
    public int          baseTowerDamage     = 2;
}
