using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float      spawnRate;
    [SerializeField] private Enemy      prefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform destination;
                     private Enemy      spawned;   
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",0f,spawnRate);
    }

    private void Spawn()
    {
     spawned = Instantiate(prefab,spawnPoint.position,Quaternion.identity,gameObject.transform);
     spawned.destination = destination;
    }
}
