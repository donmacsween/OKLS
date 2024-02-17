using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    // Singleton Instance
    public static MoneyManager         Instance { get; private set; }
    // Event to notify of money change
    public delegate void UpdateMoney();
    public static event  UpdateMoney OnMoneyUpdated;
    // Money Tick                     
    public int        moneyTickAmount = 1;
    public float      moneyTickInterval = 10f;
    public int        currentGold = 200;

    private void Awake()
    {      
        // singleton logic
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        MoneyTickStart();
    }
    public void AddMoney (int amount)
    {
        currentGold += amount;
        OnMoneyUpdated();
    }
    public void DeductMoney(int amount)
    {
        currentGold -= amount;
        if (currentGold < 0) { currentGold = 0; }
        OnMoneyUpdated();
    }
    public void MoneyTickStart () 
    {
        InvokeRepeating("MoneyTick", 0, moneyTickInterval);
    }
    private void MoneyTick()
    {
        currentGold += moneyTickAmount;
        OnMoneyUpdated();
    }
    public void MoneyTickStop()
    {
        CancelInvoke();
    }
}
