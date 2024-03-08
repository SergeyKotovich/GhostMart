using System;
using Events;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _initialMoney = 50;
    private int _money;

    private void Awake()
    {
        _money = _initialMoney;
    }

    private void Start()
    {
        EventStreams.Global.Publish(new MoneyChangedEvent(_money));
    }

    public bool HasEnoughMoney(int amount)
    {
        return _money >= amount;
    }

    public void SpendMoney(int amount)
    {
        _money -= amount;
        EventStreams.Global.Publish(new MoneyChangedEvent(_money));
    }
    public void AddMoney(int amount)
    {
        _money += amount;
        EventStreams.Global.Publish(new MoneyChangedEvent(_money));
    }
}