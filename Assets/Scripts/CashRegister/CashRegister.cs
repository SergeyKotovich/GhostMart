using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class CashRegister : MonoBehaviour, IInteractable, ICashRegister
{
    [field:SerializeField] public Sprite Icon {get; private set; }
    [field:SerializeField] public Transform PointForCustomers { get; private set; }
    [field:SerializeField] public InteractableTypes Type { get; private set; }
    [field:SerializeField] public Queue Queue { get; private set; }
    [SerializeField] private MoneySpawner _moneySpawner;
    [SerializeField] private int _delay;
    private MoneyStorage _moneyStorage;
    public bool IsAvailable { get; private set; }

    private void Awake()
    {
        Queue.Initialize(PointForCustomers);
        _moneyStorage = new MoneyStorage();
    }
    
    public async UniTask SellProducts(List<Product> products)
    {
        foreach (var product in products)
        {
            _moneySpawner.Spawn(product.Price);
            _moneyStorage.AddMoney(product.Price);
            await UniTask.Delay(_delay);
        }
    }

    public int GetMoney()
    {
        var currentAmount = _moneyStorage.GetMoney();
        _moneyStorage.ResetMoney();
        _moneySpawner.OnMoneyClaimed();
        return currentAmount;
    }
    public Vector3 GetFreePosition(ICustomer customer)
    {
        return Queue.GetFreePosition(customer);
    }

    public void OnCustomerLeft(ICustomer customer)
    {
        Queue.OnCustomerLeft(customer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG) || other.CompareTag(GlobalConstants.ASSISTANT_TAG))
        {
            IsAvailable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG) || other.CompareTag(GlobalConstants.ASSISTANT_TAG))
        {
            IsAvailable = false;
        }
    }
    
}