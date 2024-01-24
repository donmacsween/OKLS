using System;
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
                        private Transform   firingPoint           = null; // a transform on the tower weapon from which ammunition is fired
                        private List<Enemy> targetlist            = null; 
                        private bool        hasActiveTargets      = false;
                        private bool        targetIsAquired       = false;
                        private bool        isDestroyed           = false;
                        private Transform   currentTargetLocation = null;
                        private float       singleStep            = 0f;
                        private Vector3     targetDirection;
                        private Vector3     newDirection;

    private void Awake()
    {
        if (targetZone == null)
        {
            // The target zone gameobject MUST be the first child of the tower
            targetZone = this.gameObject.transform.GetChild(0);
            if (targetZone != null )
            {
                Debug.LogError("No target zone on " + this.name + " tower");
            }


        }

    }

    void Update()
    {
        if (hasActiveTargets)
        {
            MaintainTargetList();
            SelectTarget();
            if (targetIsAquired) // firing time
            {
                FireAtTarget();
            }
        }
    }

    

    private void MaintainTargetList()
    {
        hasActiveTargets = false;
        for (int i = 0; i < targetlist.Count; i++)
        {
            if (targetlist[i].isDead)
            {
                RemoveFromTargetList(i);
                // score
            }
            else
            {
                hasActiveTargets = true; // check logic
            }
        }
    }
    public void AddToTargetList(Enemy enemy)
    {
        // this is called from the targetzone gameobject's collider when a enemy enters the trigger zone
        targetlist.Add(enemy);
        hasActiveTargets = true;
    }

    public void EnemyLeftTargetZone(Enemy target)
    {
        // this is called from the targetzone gameobject's collider when a enemy leaves the trigger zone
        targetlist.Remove(target);
    }

    private void RemoveFromTargetList(int indexPosition)
    {
        targetlist.RemoveAt(indexPosition);
    }
    

    private void SelectTarget()
    {
        currentTargetLocation = targetlist[0].transform;
        // raycast to target

    }

    private void RotateToTarget(Transform target)
    {
        targetDirection     = target.position - transform.position;
        singleStep          = turnSpeed * Time.deltaTime;
        newDirection        = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation  = Quaternion.LookRotation(newDirection);
#if UNITY_EDITOR
        Debug.DrawRay(transform.position, newDirection, Color.red);
#endif
    }

    private void FireAtTarget()
    {
        //
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
