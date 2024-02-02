using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject TowerPurchasePanel;
    public GameObject LevelStartPanel;
    public GameObject TowerUpgradePanel;
    public GameObject WinPanel;
    public GameObject DefeatPanel;
    public GameObject DialogPanel;
    public GameObject ActivePanel;
    


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
    }
    public void HidePanel(GameObject panel) 
    {
        ActivePanel.SetActive(false);
        ActivePanel = null;
    }


}