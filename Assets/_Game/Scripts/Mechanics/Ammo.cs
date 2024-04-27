using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float ammoDamage  = 10f;
    public float armourDamage = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(ammoDamage);
            enemy.ArmourDamage(armourDamage);
            Destroy(this.gameObject);
        }
    }
}
