using UnityEngine;

public class TargetZone : MonoBehaviour
{
    [SerializeField] private Tower attachedTower;
    void Awake()                
    {
        if (attachedTower == null) { attachedTower = transform.parent.gameObject.GetComponent<Tower>(); }   
    }    
    //These events are prefiltered to ojects on the "Enemies" layer only.
    private void OnTriggerEnter (Collider other) { attachedTower.AddToTargetList(other.gameObject.GetComponent<Enemy>() ); }
    private void OnTriggerExit  (Collider other) { 
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        attachedTower.RemoveFromTargetList(enemy );
        attachedTower.TakeDamage(enemy.baseDamage);
    }       
}
