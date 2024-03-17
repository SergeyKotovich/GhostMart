using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class CashRegister : MonoBehaviour, IInteractable, ICashRegister
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
    
    public async UniTask SellProducts(List<Product> products)
    {
        foreach (var product in products)
        {
            _moneySpawner.AddMoney(product.Price);
            await UniTask.Delay(100);
        }
        
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