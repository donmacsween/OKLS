using UnityEngine;
using UnityEngine.AI;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Transform[] nextWayPoints;
                     private int         randomChoice;
                     private Enemy enemy;
    // incorporate a bias here

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.GetComponent<Enemy>();
            if (nextWayPoints.Length >1) 
            {
                randomChoice = Random.Range(0, nextWayPoints.Length);// incorporate a bias here
            }
            else
            {
                randomChoice = 0;
            }
            enemy.SetDestination(nextWayPoints[randomChoice]);
        }
    }
}
