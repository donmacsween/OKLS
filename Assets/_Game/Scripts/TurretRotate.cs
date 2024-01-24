using UnityEngine;

public class TurretRotate : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float baseSpeed = 1.0f;
    private Vector3 targetDirection;
    private Vector3 newDirection;
    private float singleStep;

    void Update()
    {
        RotateToTarget(target);
    }

    private void RotateToTarget(Transform currentTarget)
    {
        targetDirection     = currentTarget.position - currentTarget.position;
        singleStep          = baseSpeed * Time.deltaTime;
        newDirection        = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation  = Quaternion.LookRotation(newDirection);
    #if UNITY_EDITOR
            Debug.DrawRay(transform.position, newDirection, Color.red);
    #endif
    }


}




// https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html

