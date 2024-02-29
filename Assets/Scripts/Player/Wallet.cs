using UnityEngine;

public class Wallet
{
    public int Money { get; private set; }
    public bool HasEnoughMoney(int amount)
    {
        return Money >= amount;
    }

    public void SpendMoney(int amount)
    {
        Money -= amount;
        Debug.Log("money value = " + Money);
        //TODO: event to ui Money Were changed Update MoneyView using tier
    }
    public void AddMoney(int amount)
    {
        Money += amount;
        Debug.Log("money value = " + Money);
        //TODO: event to ui Money Were changed Update MoneyView using tier
    }
}