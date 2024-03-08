using System;
using Events;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _money;
    private IDisposable _subscription;

    private void Awake()
    {
        _subscription = EventStreams.Global.Subscribe<MoneyChangedEvent>(UpdateMoneyView);
    }

    private void UpdateMoneyView(MoneyChangedEvent moneyChangedEvent)
    {
        _money.text = moneyChangedEvent.Money.ToString();
    }
    
    private void OnDestroy()
    {
        _subscription.Dispose();
    }
    
}