using UnityEngine;
using UnityEngine.AI;

    public class Enemy : MonoBehaviour
    {
                     public EnemySO         enemySO;    
                     public bool            isDead     = false;
                     public float           health     = 20f;
                     public int             baseDamage = 5;
                     public Transform       destination;
                     private NavMeshAgent   agent;

    private void Awake()
    {
        if(enemySO == null)
        {
            Debug.LogError("No scriptable object set on :" + this.gameObject.name);
        }
        agent    = GetComponent<NavMeshAgent>();
    }
    public void SetDestination (Transform destination)
    {
        agent.destination = destination.position;
    }
    public void TakeDamage(float damage)
    {
        health -= damage-enemySO.baseArmour;
        if (health <= 0f) { Die();}
    }
    private void Die()
    {
        isDead = true;
        agent.isStopped = true; 
        this.gameObject.SetActive(false);
        SpawnManager.Instance.DespawnEnemy(this);
        MoneyManager.Instance.AddMoney(baseDamage);
        Destroy(this.gameObject, .5f);
    }
}

