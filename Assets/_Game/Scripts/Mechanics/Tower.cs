using System;
using System.Collections.Generic;
using UnityEngine;


    public class Tower : MonoBehaviour
    {
        
    [SerializeField]    private TowerTypeSO     towerType;

    //Health
                        public float            health                  = 0f;
                        public float            maxHealth               = 0f;
                        public float           healthUpgradeLevel      = 0f;
                        //FireRate
                        private float           fireRate                = 0f;
                        private float           fireRateUpgradeLevel    = 1f;
                        //Range
                        private float           range                   = 0f;
                        public float           rangeUpgradeLevel       = 1f;
                        //Damage
                        private float           damageMultiplier        = 1f;
                        private float           damageUpgradeLevel      = 1f;

                        private float           upgradeInprovement = 20f;
                        public int towerCost;
                        private float           repairCostMultiplier    = 0.5f;
                        public  TowerBase       towerBase;
    [SerializeField]    private float           turnSpeed               = 10f;
    [SerializeField]    private float           heightOffset            = 2f;
    [SerializeField]    private Rigidbody       ammoPrefab;
    [SerializeField]    private float           ammoVelocity            = 30f;
    [SerializeField]    private Transform       targetZone              = null; // a cylindrical mesh colliider used to detecect enemies
    [SerializeField]    private Transform       firingPoint             = null; // a transform on the tower weapon from which ammunition is fired
    [SerializeField]    private LayerMask       raycastMask;                    // a mask used to only target enemies
                        private List<Enemy>     targetList              = new List<Enemy>();
    [SerializeField]    private Vector3         targetOffset;
                        private Transform       currentTargetLocation   = null;
                        private float           singleStep              = 20f;
                        private Vector3         targetDirection;                // The vector of the  current  target
                        private RaycastHit      hit;
                        private float           nextFire                = 0.0f;

    [SerializeField]    private AudioClip[]     fireSounds;
    [SerializeField]    private AudioSource     soundSource;


        private void Awake()
        {
        if (soundSource == null) {soundSource = GetComponent<AudioSource>();}

        health   = towerType.health;
        maxHealth = health;
        fireRate = towerType.fireRate;
        range    = towerType.range;
        fireSounds = towerType.fireSounds;
        towerBase = TowerManager.Instance.activeTowerBase;
        towerCost = towerType.towerCost;
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
            if (targetList.Count > 0)
            {
                for (int i = 0; i < targetList.Count; i++)
                {
                    if (targetList[i].isDead) {targetList.RemoveAt(i);}
                }
            }
        }
        // this is called from the targetzone gameobject's collider when a enemy enters the trigger zone
        public void AddToTargetList(Enemy target) { targetList.Add(target); }

        // this is called from the targetzone gameobject's collider when a enemy leaves the trigger zone
        public void RemoveFromTargetList(Enemy target) { targetList.Remove(target); }
        private void SelectTarget()
        {
            if (targetList.Count > 0)
            {
                currentTargetLocation = targetList[0].transform;
                RotateToTarget(currentTargetLocation);
                Physics.Raycast(firingPoint.position, firingPoint.TransformDirection(Vector3.forward), out hit, range, raycastMask);
                Debug.DrawRay(firingPoint.position, firingPoint.TransformDirection(Vector3.forward) * range, Color.white);
                if (hit.collider != null) {FireAtTarget();}
            }
        }

        private void RotateToTarget(Transform target)
        {
            targetDirection = target.position - transform.position;
            targetDirection.y = targetDirection.y - heightOffset;
            singleStep = turnSpeed * Time.deltaTime;
            var rotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, singleStep);
        }

        private void FireAtTarget()
        {
            if (Time.time > fireRate + nextFire)
            {
                Rigidbody hitPlayer;
                hitPlayer = Instantiate(ammoPrefab, firingPoint.position, transform.rotation) as Rigidbody;
                hitPlayer.gameObject.GetComponent<Ammo>().ammoDamage = hitPlayer.gameObject.GetComponent<Ammo>().ammoDamage * damageMultiplier;
                hitPlayer.velocity = transform.TransformDirection((Vector3.forward + targetOffset) * ammoVelocity);
                nextFire = Time.time;
                if (towerType.fireSounds.Length>0) 
                {
                AudioSource.PlayClipAtPoint(towerType.fireSounds[UnityEngine.Random.Range(0, towerType.fireSounds.Length - 1)], gameObject.transform.position);
                }
            }
        }

        // Tower Damage
        public void TakeDamage(float damage)
        {
            health = health - (damage);
            if (health < 0) {DestroyTower();}
        }

       
        private void DestroyTower()
        {
            // play destruction anim
            // Clear the towerbase flag so we can build again
            towerBase.built = false;
            // Remove this tower from the list of alive towers
            TowerManager.Instance.towers.Remove(this);
            // Remove the gameobject from the scene
            Destroy(this.transform.parent.gameObject);
            // Record in the stats that we have lost a tower
            ObjectiveManager.Instance.towersLost++;
        }

        // Player Actions

        public void PlayerDestroyTower()
        {
        // play destruction anim
        // Calculate the cashback based on % health
        float returnPercent = health * 100 / maxHealth;
        float towerpercent = (float)towerType.towerCost / 100;
        float cashback = towerpercent * returnPercent;
        
        MoneyManager.Instance.AddMoney(Convert.ToInt32(cashback));
        towerBase.built = false;
        // Remove the gameobject from the scene
        Destroy(this.transform.parent.gameObject);
    }

    
        public int GetRepairCost()
        {
            return Mathf.RoundToInt((maxHealth - health) * repairCostMultiplier);
        }

        public int GetTowerHealth()
        {
            return Mathf.RoundToInt(health);
        }

    public void UpgradeHealth()
    {
        if (healthUpgradeLevel < 4)
        {
            float factor = maxHealth / 100 * upgradeInprovement;
            maxHealth = maxHealth + factor;
            health = health + factor;
            if (health > maxHealth) { health = maxHealth; }
            healthUpgradeLevel++;
        }
    }


    }

