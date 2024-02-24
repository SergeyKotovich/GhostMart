using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class CollectingProductsState : MonoBehaviour , IState
{
    [SerializeField] private CollectingProducts _collectingProducts;
    [SerializeField] private ProductFactory _productFactory;
    
    private ICollectable _assistant;
    private StateMachine _stateMachine;
    private bool _canPickUp;

    private void Awake()
    {
        _assistant = GetComponent<ICollectable>();
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _canPickUp = true;
    }

    private void Update()
    {
        if (_canPickUp)
        {
            CollectingProduct();
        }
    }

    private void CollectingProduct()
    {
        if (!_productFactory.HasSpawnedProduct())
        {
            return;
        }
        
        if (_assistant.WorkerBasket.IsFull())
        {
            _canPickUp = false;
            _stateMachine.Enter<AssistantMovingToTargetState>();
            return;
        }
        
        var product = _productFactory.GetProduct();
        _collectingProducts.SetPosition(product);
        _assistant.WorkerBasket.AddProductInBasket(product);
    }

}