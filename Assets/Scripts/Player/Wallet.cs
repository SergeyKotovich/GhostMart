using Events;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _initialMoney = 50;
    [SerializeField] private TextMeshProUGUI _money;
    public int Money { get; private set; }

    private void Awake()
    {
        Money = _initialMoney;
        _money.text = Money.ToString();
    }

    public bool HasEnoughMoney(int amount)
    {
        return Money >= amount;
    }

    public void SpendMoney(int amount)
    {
        Money -= amount;
        _money.text = Money.ToString();
        EventStreams.Global.Publish(new MoneyChangedEvent(Money));
    }
    public void AddMoney(int amount)
    {
        Money += amount;
        EventStreams.Global.Publish(new MoneyChangedEvent(Money));
    }
}