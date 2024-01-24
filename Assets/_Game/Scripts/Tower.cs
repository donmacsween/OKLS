using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
                        private List<Enemy> targetList            = new List<Enemy>(); 
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
            if (targetZone != null ) {Debug.LogError("No target zone on " + this.name + " tower");}
        }
    }
    void Update()
    {
        if (targetList.Count > 0)
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
        for (int i = 0; i < targetList.Count; i++)
        {
            if (targetList[i].isDead)
            {
                Debug.Log(targetList[i].gameObject.name + " died");
                targetList.RemoveAt(i);
            }
        }   
    }
    public void AddToTargetList(Enemy target)
    {
        // this is called from the targetzone gameobject's collider when a enemy enters the trigger zone
        Debug.Log(target.gameObject.name + " entered");
        targetList.Add(target);
    }
    public void RemoveFromTargetList(Enemy target)
    {
        // this is called from the targetzone gameobject's collider when a enemy leaves the trigger zone
        Debug.Log(target.gameObject.name + " escaped");
        targetList.Remove(target);
    }
    private void SelectTarget()
    {
        currentTargetLocation = targetList[0].transform;
        RotateToTarget(currentTargetLocation);
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
