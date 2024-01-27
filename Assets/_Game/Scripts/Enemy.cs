using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public  bool isDead = false;
    public float health = 20f;
    public Transform destination;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            agent.destination = destination.position;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            isDead = true;
            agent.isStopped=true;
            Destroy(this.gameObject,1f);
            
        }
    }
}
