using UnityEngine;
using UnityEngine.AI;

    public class Enemy : MonoBehaviour
    {
                     public EnemySO      enemySO;    
                     public bool         isDead     = false;
                     public float        health     = 20f;
                     public int           baseDamage = 5;
                     public Transform     destination;
                     private NavMeshAgent agent;
                     private Animator     animator;

    private void Awake()
    {
        if(enemySO == null)
        {
            Debug.LogError("No scriptable object set on :" + this.gameObject.name);
        }
        else
        {

        }

        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            agent.destination = destination.position;
            // need to do ev frame ???
        }
    }
    public void SetDestination (Transform destination)
    {
        agent.destination = destination.position;
    }
    public void TakeDamage(float damage)
    {
        health -= damage-enemySO.baseArmour;
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
        this.gameObject.SetActive(false);
        SpawnManager.Instance.DespawnEnemy(this);
        MoneyManager.Instance.AddMoney(baseDamage);
        Destroy(this.gameObject, .5f);
        // play death animation
        // do return to pool
    }
    private void Success()
    {
        isDead = true;
        agent.isStopped = true;
        this.gameObject.SetActive(false);
        SpawnManager.Instance.DespawnEnemy(this);
        
        Destroy(this.gameObject, .5f);
        // play death animation
        // do return to pool
    }



    private void AttackTower()
    { 
    // pending
    }

    public void Dissolve()
    {
    // pending
    }
   
}

