using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   
    // Event to notify of money change
    
    public static SpawnManager Instance { get; private set; }
    public delegate void UpdateSpawnData();
    public static event UpdateSpawnData OnSpawnDataUpdated;

    // For HUD data
    public List<Enemy> activeEnemies;
    public int wavePopulation = 0;
    public int wavesRemaining = 0;

    [SerializeField] private float       spawnRate;
    [SerializeField] private Transform   sceneHolder;
    [SerializeField] private Enemy       prefab;
    [SerializeField] private Transform   spawnPoint;
    [SerializeField] private Transform   destination;
                     private Enemy       spawned;
    [SerializeField] private WaveSO[]    waves;
    [SerializeField] private int         currentWave = 0;
    [SerializeField] private int         currentEnemy = 0;
    
                     
   

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
        wavesRemaining = waves.Length;
    }
         
    public void StartWave(int waveNumber)
    {
        wavePopulation = waves[currentWave].Enemies.Length;
        currentWave = waveNumber;
        currentEnemy = 0;
        InvokeRepeating("SpawnWave", 0f, waves[currentWave].spawnInterval);
    }
    private void EndWave() 
    { 
        CancelInvoke();
        // if last wave
        //  Award money
        //  Show UI
        //  OnSpawnDataUpdated();
    }



    private void SpawnEnemy()
    {
        if (currentEnemy < wavePopulation) 
        {
            spawned = Instantiate
            (
            waves[currentWave].Enemies[currentEnemy].enemyPrefab.GetComponent<Enemy>(),
            spawnPoint.position,
            Quaternion.identity,
            gameObject.transform
            );
            currentEnemy++;
            spawned.destination = destination;
            activeEnemies.Add(spawned);
            OnSpawnDataUpdated();
        }
    }

    public void EnemyKilled(Enemy enemy)
    { 
    activeEnemies.Remove(enemy);
        OnSpawnDataUpdated();
        if (activeEnemies.Count == 0)
        {
            EndWave();
        }
    }

    
}
