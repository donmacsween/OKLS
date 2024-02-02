using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public Transform buildPoint;
    public float     startingYRotation = 0;
    public bool      built = false;

    private void Awake()
    {
        if (buildPoint == null)
        {
            Debug.LogError("build point not set on" + this.gameObject.name);
        }
    }
}
