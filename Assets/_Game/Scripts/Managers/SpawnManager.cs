using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{  
    public static        SpawnManager       Instance { get; private set; }
    public delegate void UpdateSpawnData();
    public static event  UpdateSpawnData    OnSpawnDataUpdated;

    // For HUD data
    public List<Enemy>  activeEnemies;
    public int          wavePopulation  = 0;
    public int          totalWaves      = 0;
    public int          currentWave     = 0;
    public int          currentEnemy    = 0;
    public int          waveBonus       = 0;

    [SerializeField] private float       spawnRate;
    [SerializeField] private Transform   sceneHolder;
    [SerializeField] private Enemy       prefab;
    [SerializeField] private Transform   spawnPoint;
    [SerializeField] private Transform   destination;
                     private Enemy       spawned;
    [SerializeField] private WaveSO[]    waves;
    [SerializeField] private EnemySO[]   spawnedEnemy;

    private void Awake()
    {
        // singleton logic
        if (Instance != null && Instance != this) {Destroy(this);}
        else {Instance = this;}
        totalWaves = waves.Length;
        activeEnemies = new List<Enemy>();
    }

    private void Start()
    {
        UIManager.Instance.ShowPanel(UIManager.Instance.DialogPanel);
    }

    public void StartWave()
    {
        wavePopulation = waves[currentWave].Enemies.Length;
        waveBonus = waves[currentWave].waveBonus;
        currentEnemy = 0;
        InvokeRepeating("SpawnEnemy", 1f, waves[currentWave].spawnInterval);
    }
    private void EndWave() 
    { 
        
        CancelInvoke();
        currentWave++;
        // Give the end of wave bonus
        if (currentWave < totalWaves)
        {
            StartWave();
            OnSpawnDataUpdated();
        }
        else 
        { 
            CancelInvoke();
            //Win Panel
        }
    }
    private void SpawnEnemy()
    {
        if (currentEnemy < wavePopulation) 
        {
            prefab = waves[currentWave].Enemies[currentEnemy].enemyPrefab.GetComponent<Enemy>();
            spawned = Instantiate (prefab, spawnPoint.position, Quaternion.identity, sceneHolder);
            spawned.gameObject.SetActive(true);    
            spawned.SetDestination(destination);   
            spawned.GetComponent<NavMeshAgent>().speed = waves[currentWave].Enemies[currentEnemy].baseSpeed;
            spawned.health = waves[currentWave].Enemies[currentEnemy].baseHealth;
            prefab.enemySO = waves[currentWave].Enemies[currentEnemy];
            prefab.baseDamage = waves[currentWave].Enemies[currentEnemy].baseCastleDamage;
            activeEnemies.Add(spawned);
            currentEnemy++;
            OnSpawnDataUpdated();
        }
        else {EndWave();}
    }
    public void DespawnEnemy(Enemy enemy)
    { 
        activeEnemies.Remove(enemy);
        OnSpawnDataUpdated();
        // check for win condition
        if (activeEnemies.Count == 0)
        {
            UIManager.Instance.ShowPanel(UIManager.Instance.WinPanel);
            AudioManager.Instance.WinMusic();
            EndWave();
        }
    }
}
