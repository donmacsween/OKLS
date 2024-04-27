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
                     private float          armour;
                     private Animator       animator;
                     private bool           slowed = false;
                    private float currentSpeed;

    private void Awake()
    {
        if(enemySO == null)
        {
            Debug.LogError("No scriptable object set on :" + this.gameObject.name);
        }
        agent    = GetComponent<NavMeshAgent>();
        armour = enemySO.baseArmour;
        animator = GetComponent<Animator>();
    }
    public void SetDestination (Transform destination)
    {
        agent.destination = destination.position;
    }
    public void TakeDamage(float damage)
    {
        health -= damage-armour;
        if (health <= 0f) { Die();}
    }
    public void ArmourDamage(float armourDamage)
    {
       armour -= armourDamage;
        if (armour <= 0)
        {
            armour = 0;
        }
    }

    public void Slow(float multiplier, float duration)
    {
        if (!slowed)
        {
            agent.speed /= multiplier;
            animator.SetFloat("speed", agent.speed /= multiplier);
            slowed = true;
            Invoke("Unslow", duration);
        }
    }

    private void Unslow() 
    {
        Debug.Log("unslowed"); 
        agent.speed = enemySO.baseSpeed;
        animator.SetFloat("speed", enemySO.baseSpeed);
        slowed = false;
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

