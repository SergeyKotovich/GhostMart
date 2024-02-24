using System;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _money;
    
    private void Start()
    {
        MoneyBonus.MoneyAdd += AddMoney;
    }

    private void AddMoney(int value)
    {
        var moneyInWallet = Convert.ToInt32(_money.text);
        var money = moneyInWallet + value;
        _money.text = money.ToString();
    }
    
    private void OnDestroy()
    {
        MoneyBonus.MoneyAdd -= AddMoney;
    }
    
}