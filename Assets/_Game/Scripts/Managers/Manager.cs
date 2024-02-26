// Author      : Don MacSween
// Liscence    : All rights reserved.
// Purpose     : The overall manager for the game
// Dependencies: None

// Namespaces
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Properties
    public static Manager  Instance     { get; private set; }
    public        MoneyManager MoneyManager { get; private set; }
    public        BaseManager  BaseManager  { get; private set; }
    public        TowerManager TowerManager { get; private set; }
    public        UIManager    UIManager    { get; private set; }
    public        SpawnManager SpawnManager { get; private set; }


    // Unity Methods
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        EnforceSingleton();
        ObjectReferenceChecks();
    }

    // Public Methods 
    public void LevelComplete() { }
    public void PauseGame() { }
    public void ResumeGame() { }
    public void QuitGame() { }
    public void LoadLevel(string levelName)
    {
        // change to generic music
        // Set loading screeen BG
        // show loading panel
        StartCoroutine(LoadLevelAsync(levelName));
        // Do post load things
        // Show Starting Dialog
    }
    
    IEnumerator LoadLevelAsync(string levelName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void EnforceSingleton()
    {
        if (Instance != null && Instance != this)
        {Destroy(this);}
        else
        {Instance = this;}
    }

    private void ObjectReferenceChecks()
    {
        if (MoneyManager == null)
        {
            if (gameObject.TryGetComponent<MoneyManager>(out MoneyManager moneyManager))
            { MoneyManager = moneyManager; }
            else
            { MoneyManager = gameObject.AddComponent<MoneyManager>(); }
        }
    }


}
