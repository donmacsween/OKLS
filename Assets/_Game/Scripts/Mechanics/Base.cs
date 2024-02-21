using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Base : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // prefiltered to enemy layer
        // Tell the BaseManager.cs to deduct health equivelent to enemy's baseDamage
        BaseManager.Instance.DeductHealth(other.gameObject.GetComponent<Enemy>().baseDamage);
        SpawnManager.Instance.DespawnEnemy(other.GetComponent<Enemy>());
    }
}
