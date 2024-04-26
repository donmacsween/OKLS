using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace OKLS
{
    public class CurrentSpawnStats : MonoBehaviour
    {
        [SerializeField] private TMP_Text wavesRemainingLabel;
        [SerializeField] private TMP_Text enemyCount;
        [SerializeField] private Slider   slider;
                         private int      wavesRemaining;
                         private int      enemiesInWave;
                         private int      enemiesOut;
        private void OnEnable() { SpawnManager.OnSpawnDataUpdated += UpdateStats; }
        private void OnDisable() { SpawnManager.OnSpawnDataUpdated += UpdateStats; }
        private void Start() { UpdateStats(); }
        void UpdateStats()
        {
            // Get data from SpawnManageer.cs
            wavesRemaining = (SpawnManager.Instance.totalWaves - SpawnManager.Instance.currentWave);
            //wavesRemaining = (SpawnManager.Instance.totalWaves - SpawnManager.Instance.currentWave) - 1;
            enemiesInWave = SpawnManager.Instance.wavePopulation;
            enemiesOut = SpawnManager.Instance.currentEnemy;
            // Change UI
            wavesRemainingLabel.text = wavesRemaining.ToString();
            enemyCount.text = enemiesOut.ToString() + "/" + enemiesInWave.ToString();
            slider.maxValue = enemiesInWave;
            slider.minValue = 0f;
            slider.value = enemiesOut;
        }
    }
}
