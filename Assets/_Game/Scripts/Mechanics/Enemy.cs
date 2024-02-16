using UnityEngine;
using UnityEngine.AI;

    public class Enemy : MonoBehaviour
    {
        
    public bool          isDead     = false;
    public float         health     = 20f;
    public int           baseDamage = 5;
    public Transform     destination;
    private NavMeshAgent agent;
    private Animator     animator;

    private void Awake()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            agent.destination = destination.position;
        }
    }
    public void SetDestination (Transform destination)
    {

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f) { Die();}
        else 
        {
            // do hit animation        
        }
    }

    private void Die()
    {
    isDead = true;
    agent.isStopped = true;
    Destroy(this.gameObject, 1f);
    // play death animation
    // do return to pool
    }

    public void Dissolve()
    {
        
    }
}

