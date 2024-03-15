// Author      : Don MacSween
// Liscence    : All rights reserved.
// Purpose     : The overall manager for the game
// Dependencies: None

// Namespaces
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Properties
    public static Manager  Instance     { get; private set; }
    
    // Unity Methods
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        EnforceSingleton();
        
    }

    // Public Methods 
    public void LevelComplete() { }
    public void PauseGame() { }
    public void ResumeGame() { }
    public void QuitGame() 
    { 
    Application.Quit();
    }
    public void LoadLevel(LevelSO levelSO)
    {
        // change to generic music
        // Set loading screeen BG
        // show loading panel
        StartCoroutine(LoadLevelAsync(levelSO.name));
        MoneyManager.Instance.currentGold = levelSO.startingGold;
        BaseManager.Instance.SetHealth(levelSO.startingHealth);
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



}
