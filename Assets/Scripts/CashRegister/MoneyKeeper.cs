using Events;

public class MoneyKeeper
{
    public int CurrentMoneyAmount { get; private set; }
    public int PreviousMoneyValue { get; set; }

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
        PreviousMoneyValue = 0;
    }
}