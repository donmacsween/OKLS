using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AsyncLoader : MonoBehaviour
{
    public string level;
    [SerializeField] private GameObject panel; 
    [SerializeField] private GameObject mainMenu; 
    [SerializeField] private Text       levelName;

    private void Awake()
    {
        panel.SetActive(false);
    }


    public void StartLoadScene(string levelIn)
    {
        level = levelIn;
        Invoke("LoadScene",1.5f);
        
       
        

    }
    public void LoadScene()
    {
        mainMenu.SetActive(false);
        panel.SetActive(true);
        StartCoroutine(LoadAsyncScene());
    }

    public void LoadSceneNow(string nextlevel)
    {
        level = nextlevel;
        mainMenu.SetActive(false);
        panel.SetActive(true);
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            
            yield return null;
        }
    }

}
