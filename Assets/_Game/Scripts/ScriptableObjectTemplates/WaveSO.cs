using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "GameData/Wave", order = 1)]
public class WaveSO : ScriptableObject
{
    public EnemySO[] Enemies;
    public float spawnInterval = 2f;
    public int waveBonus = 100;
    
}
