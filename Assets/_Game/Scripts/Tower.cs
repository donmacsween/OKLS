using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Tower Attributes                                                     All of the properties below have values allocated by the towertype
    //                                                                      and Ammo scriptableObjects.
    // [SerializeField] private TurretType turretType;
    // [SerializeField] private string      ammoType; //  change to SO
                        private float       Maxhealth             = 100f; 
                        private float       health                = 100f; 
                        private float       armour                = 5f;
                        private float       turnSpeed             = 1f;
                        private float       fireRate              = 1f;
                        private float       range                 = 10f;
                        private float       damageMultiplier      = 1f;
                        private float       repairCostMultiplier  = 0.5f;
    // Tower mechanics
    [SerializeField]    private Transform   targetZone            = null;
    [SerializeField]    private Transform   firingPoint           = null; // a transform on the tower weapon from which ammunition is fired
                        private List<Enemy> targetList            = new List<Enemy>(); 
                        private bool        hasActiveTargets      = false;
                        private bool        targetIsAquired       = false;
                        private bool        isDestroyed           = false;
                        private Transform   currentTargetLocation = null;
                        private float       singleStep            = 0f;
                        private Vector3     targetDirection;
                        private Vector3     newDirection;
                        private float       nextFire = 0.0f;
                        

    private void Awake()
    {
        if (targetZone == null)
        {
            // The target zone gameobject MUST be the first child of the tower
            targetZone = this.gameObject.transform.GetChild(0);
            if (targetZone == null ) {Debug.LogError("No target zone on " + this.name + " tower");}
        }
        if (firingPoint == null)
        {
            // The firingPoint gameobject MUST be the secong child of the tower
            firingPoint = this.gameObject.transform.GetChild(1);
            if (firingPoint == null) { Debug.LogError("No firing Point on " + this.name + " tower"); }
        }

        // Apply SOs here
    }
    void Update()
    {
        if (targetList.Count > 0)
        {
            MaintainTargetList();
            SelectTarget();
        }
    }
    private void MaintainTargetList()
    {     
        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].isDead)
            {
                Debug.Log(targetList[i].gameObject.name + " died");
                targetList.RemoveAt(i);
            }
        }   
    }
    // this is called from the targetzone gameobject's collider when a enemy enters the trigger zone
    public void AddToTargetList(Enemy target) {targetList.Add(target);}

    // this is called from the targetzone gameobject's collider when a enemy leaves the trigger zone
    public void RemoveFromTargetList(Enemy target) {targetList.Remove(target);}
    private void SelectTarget()
    {
        currentTargetLocation = targetList[0].transform;
        //  Raycast to selected target
        //  if (target visible)
        //  {
        //  FireAtTarget()
        //  }
        //  else
        //  {
            RotateToTarget(currentTargetLocation);
        //  }
    }

    private void RotateToTarget(Transform target)
    {
        targetDirection     = target.position - transform.position;
        singleStep          = turnSpeed * Time.deltaTime;
        newDirection        = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation  = Quaternion.LookRotation(newDirection);
    }

    private void FireAtTarget()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            // Instantiate(projectile, transform.position, transform.rotation);
            //  pooling
        }
    }

    // Tower Damage
    public void TakeDamage (float damage)
    {
        health = health - (damage - armour);
        if (health < 0)
        {
            DestroyTower();
        }
    }

    public void TakeDamageOverTime  (float damage, float duration)
    {
    // later
    }

    private void DestroyTower()
    {
        // play destruction anim
        // 
    }

    // Player Actions

    private void PlayerDestroyTower()
    {
        // play destruction anim
        // 
    }

    public void PlayerApplyUpgrade()
    {
        //later
    }
    public int GetRepairCost()
    {
        return Mathf.RoundToInt((Maxhealth - health) * repairCostMultiplier);
    }

    public int GetTowerHealth ()
    {
        return Mathf.RoundToInt(health);
    }

    public void PlayerRepairTower ()
    {
        // later
    }

}
