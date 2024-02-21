
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentSpawnStats : MonoBehaviour
{
    [SerializeField] private TMP_Text wavesRemainingLabel;
    [SerializeField] private TMP_Text enemiesInWaveLabel;
    [SerializeField] private TMP_Text enemiesOutLabel;
    [SerializeField] private Slider slider;
                     private int wavesRemaining;
                     private int enemiesInWave;
                     private int enemiesOut;

    private void OnEnable()
    {
        SpawnManager.OnSpawnDataUpdated += UpdateStats;
    }
    private void OnDisable()
    {
        SpawnManager.OnSpawnDataUpdated += UpdateStats;
    }

    // Start is called before the first frame update
    void Start()
    {
         UpdateStats();
    }

    // Update is called once per frame
    void UpdateStats()
    {
    wavesRemaining = SpawnManager.Instance.totalWaves - SpawnManager.Instance.currentWave;
    enemiesInWave  = SpawnManager.Instance.wavePopulation;
    enemiesOut     = SpawnManager.Instance.currentEnemy;
    wavesRemainingLabel.text = wavesRemaining.ToString();
    enemiesInWaveLabel.text  = enemiesInWave.ToString();
    enemiesOutLabel.text     = enemiesOut.ToString();

        slider.maxValue = enemiesInWave;
        slider.minValue = 0f;
        slider.value    = enemiesOut;
    }
}
