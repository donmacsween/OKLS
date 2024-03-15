using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveEnd : MonoBehaviour
{
    TMP_Text bonus;

    private void Awake()
    {
        bonus.text = SpawnManager.Instance.waveBonus.ToString() + " bonus";
        MoneyManager.Instance.AddMoney(SpawnManager.Instance.waveBonus);
        Invoke("NextWave", 5f);
    }

    private void NextWave()
    {
        
    }
}
