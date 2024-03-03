using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class CashRegister : MonoBehaviour, IInteractable
{
    [field:SerializeField] public Sprite StandIcon {get; private set; }
    [field:SerializeField] public Transform PointForCustomers { get; private set; }
    [field:SerializeField] public TypeInteractablePoints TypeInteractablePoint { get; private set; }
    [field:SerializeField] public Queue Queue { get; private set; }

    [SerializeField] private MoneySpawner _moneySpawner;
    public bool IsAvailable { get; private set; }

    private void Awake()
    {
        Queue.Initialize(PointForCustomers);
    }

    public void Open()
    {
        IsAvailable = true;
    }

    public void CLose()
    {
        IsAvailable = false;
    }

    public void SellProducts(int amount)
    {
        _moneySpawner.AddMoney(amount);
    }
}