using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject TowerPurchasePanel;
    public GameObject ObjectivesPanel;
    public GameObject PowersShopPanel;
    public GameObject TowerUpgradePanel;
    public GameObject WinPanel;
    public GameObject DefeatPanel;
    public GameObject DialogPanel;
    public GameObject WavePanel;
    public GameObject PausePanel;
    public GameObject ActivePanel;
    public DialogSO   currentDialog;
    


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ShowPanel(GameObject panel)
    {
        if (ActivePanel != null) { ActivePanel.SetActive(false); }
        ActivePanel = panel;
        panel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void HidePanel() 
    {
        ActivePanel.SetActive(false);
        ActivePanel = null; 
        Time.timeScale = 1.0f;

    }


}