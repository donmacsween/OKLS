using UnityEngine;

public class BaseManager : MonoBehaviour
{
    // Singleton Instance
    public static           BaseManager Instance { get; private set; }
    // Used to notify the GUI elements that health has changed & to update
    public delegate void    UpdateBaseHealth();
    public static event     UpdateBaseHealth OnBaseHealthUpdated;
    public int              currentHealth = 200;
    private int             maxHealth = 0;
    // This is set by the GameManager.cs on level load
    public                  Transform baseTransform;


    private void Awake()
    {
        // singleton logic
        if (Instance != null && Instance != this) { Destroy(this); }
        else { Instance = this; }
        maxHealth = currentHealth;
    }
    
    // Used by LevelManager.cs to set the base health at the start of a level. 
    public void SetHealth(int amount)
    {
        currentHealth = amount;
        maxHealth = amount;
        OnBaseHealthUpdated();
    }

    // Used by Base.cs whenever a enemy makes it to the base.
    public void DeductHealth(int amount)
    {
        bool oneShot = true;
        currentHealth -= amount;
        
        if (currentHealth < (maxHealth / 3) && oneShot)
        {
        AudioManager.Instance.PanicMusic();
            oneShot = false;
        }
        if (currentHealth <= 0)
        {
            UIManager.Instance.ShowPanel(UIManager.Instance.DefeatPanel);
            AudioManager.Instance.LooseMusic();
        }
        OnBaseHealthUpdated();
    }
}
