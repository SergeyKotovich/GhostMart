using System;
using Events;
using UnityEngine;

public class MoneyStorage
{
    public int CurrentMoneyAmount { get; private set; }

    public void AddMoney(int amount)
    {
        CurrentMoneyAmount += amount;
    }
    
    public int GetMoney()
    {
        return CurrentMoneyAmount;
    }

    public void ResetMoney()
    {
        CurrentMoneyAmount = 0;
    }
}