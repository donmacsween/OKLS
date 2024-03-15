using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeProperty : MonoBehaviour
{
    private Tower tower;

    private void OnEnable()
    {
        tower= TowerManager.Instance.GetActiveTower();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
