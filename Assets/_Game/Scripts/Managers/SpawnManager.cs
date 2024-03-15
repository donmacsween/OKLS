using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
   
    // Event to notify of money change
    
    public static SpawnManager Instance { get; private set; }
    public delegate void UpdateSpawnData();
    public static event UpdateSpawnData OnSpawnDataUpdated;

    // For HUD data
    public List<Enemy> activeEnemies;
    public int wavePopulation = 0;
    public int totalWaves = 0;
    public int currentWave = 0;
    public int currentEnemy = 0;
    public int waveBonus = 0;

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
        StartWave(currentWave);
        //  replace with call from UI
    }


    public void StartWave(int waveNumber)
    {
        wavePopulation = waves[currentWave].Enemies.Length;
        waveBonus = waves[currentWave].waveBonus;
        currentWave = waveNumber;
        currentEnemy = 0;
        InvokeRepeating("SpawnEnemy", 1f, waves[currentWave].spawnInterval);

    }
    private void EndWave() 
    { 
        CancelInvoke();
        
        currentWave++;
        UIManager.Instance.ShowPanel(UIManager.Instance.WavePanel);
        Debug.Log("Wave Over");
        
        if (currentWave < totalWaves)
        {
            
            StartWave(currentWave);
            OnSpawnDataUpdated();
        }
        else
        {
            Debug.Log("all out");
            CancelInvoke();
        }
    }
    private void SpawnEnemy()
    {
        if (currentEnemy < wavePopulation) 
        {
            Debug.Log(waves[currentWave].Enemies[currentEnemy].enemyPrefab.name.ToString());
            prefab = waves[currentWave].Enemies[currentEnemy].enemyPrefab.GetComponent<Enemy>();
            // Instanciate the prefab
            spawned = Instantiate
            (prefab,
            spawnPoint.position,
            Quaternion.identity,
            sceneHolder
            );
            // Set properties
            spawned.gameObject.SetActive(true);
            //spawned.destination = destination;      
            spawned.SetDestination(destination);   
            spawned.GetComponent<NavMeshAgent>().speed = waves[currentWave].Enemies[currentEnemy].baseSpeed;
            spawned.health = waves[currentWave].Enemies[currentEnemy].baseHealth;
            prefab.enemySO = waves[currentWave].Enemies[currentEnemy];
            prefab.baseDamage = waves[currentWave].Enemies[currentEnemy].baseCastleDamage;
            
            activeEnemies.Add(spawned);
            currentEnemy++;
            OnSpawnDataUpdated();

            Debug.Log("enemies" + activeEnemies.Count.ToString());
        }
        else
        {
            EndWave();
        }
    }
    public void DespawnEnemy(Enemy enemy)
    { 
        activeEnemies.Remove(enemy);
        OnSpawnDataUpdated();
        // check for win condition
        if (activeEnemies.Count == 0)
        {
            EndWave();
        }
    }
}
