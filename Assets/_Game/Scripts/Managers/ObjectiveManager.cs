using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    public int                     towersLost     = 0;
    public int                     baseHealthLost = 0;
    public int                     enemiesKilled  = 0;


    private void Awake()
    {
        EnforceSingleton();

    }
        private void EnforceSingleton()
    {
        if (Instance != null && Instance != this) {Destroy(this);}
        else {Instance = this;}
    }
}
