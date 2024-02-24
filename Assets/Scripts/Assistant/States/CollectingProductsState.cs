using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectingProductsState : MonoBehaviour , IState
{
    [SerializeField] private ProductFactory _productFactory;
    [SerializeField] private CollectingProducts _collectingProducts;
    
    // TODO: можно ли передавать вместо асистента сразу корзину.
    // либо BasketController который хранит Basket а Player хранит BasketController
    
    // В этом стейте нам нужно использовать корзину для выполнения логики укладки продуктов на прилавок и в корзину.
    // Как правильно обратиться к корзине:
    
    // _assistant.Basket.Get/SetProduct();
    // _basketController.Basket.Get/SetProduct();
    // _assistant.BasketController.Basket.Get/SetProduct();
    
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
        if (_productFactory._allAvailableProducts.Count == 0)
        {
            return;
        }
        
        if (_assistant.Basket.IsFull())
        {
            _canPickUp = false;
            _stateMachine.Enter<AssistantMovingToTargetState>();
            return;
        }
        
        var product=_productFactory.GetProduct();
        _collectingProducts.SetPosition(product);
        _assistant.Basket.AddProductInBasket(product);
    }

}