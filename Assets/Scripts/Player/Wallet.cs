using Events;
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
        EventStreams.Global.Publish(new MoneyChangedEvent(Money));
    }
    public void AddMoney(int amount)
    {
        Money += amount;
        EventStreams.Global.Publish(new MoneyChangedEvent(Money));
    }
}