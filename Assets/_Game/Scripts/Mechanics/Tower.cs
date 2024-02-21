using System.Collections.Generic;
using UnityEngine;


    public class Tower : MonoBehaviour
    {
        // Tower Attributes                                                     All of the properties below have values allocated by the towertype
        //                                                                      and Ammo scriptableObjects.
        // [SerializeField] private TurretType turretType;
        // [SerializeField] private string      ammoType; //  change to SO
                         private float Maxhealth            = 100f;
                         private float health               = 100f;
                         private float armour               = 5f;
        [SerializeField] private float turnSpeed            = 10f;
        [SerializeField] private float heightOffset         = 2f;
        [SerializeField] private float fireRate             = .7f;
                         private float range                = 10f;
                         private float damageMultiplier     = 2f;
                         private float repairCostMultiplier = 0.5f;
        // Tower mechanics
        [SerializeField] private Rigidbody   ammoPrefab;
        [SerializeField] private float       ammoVelocity   = 30f;
        [SerializeField] private Transform   targetZone     = null;                 // a cylindrical mesh colliider used to detecect enemies
        [SerializeField] private Transform   firingPoint    = null;                 // a transform on the tower weapon from which ammunition is fired
        [SerializeField] private LayerMask   raycastMask;                           // a mask used to only target enemies
                         private List<Enemy> targetList     = new List<Enemy>();

        [SerializeField] private Vector3    targetOffset;
                         private Transform  currentTargetLocation = null;
                         private float      singleStep = 20f;
                         private Vector3    targetDirection; // The vector of the  current  target
                         private RaycastHit hit;
                         private float      nextFire = 0.0f;


        private void Awake()
        {

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
            }
        }

        // Tower Damage
        public void TakeDamage(float damage)
        {
            health = health - (damage - armour);
            if (health < 0) {DestroyTower();}
        }

        public void TakeDamageOverTime(float damage, float duration)
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

        public int GetTowerHealth()
        {
            return Mathf.RoundToInt(health);
        }

        public void PlayerRepairTower()
        {
            // later
        }
    }

