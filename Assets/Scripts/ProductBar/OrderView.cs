
using System;
using Customer;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderView : MonoBehaviour
{
    [SerializeField] private Image _productIcon;
    [SerializeField] private TextMeshProUGUI _productCount;
    private Transform _characterTransform;

    private void Awake()
    {
        EventStreams.Global.Subscribe<OrderUpdatedEvent>(OnOrderUpdated);
    }

    public void Init(Transform transform)
    {
        _characterTransform = transform;
    }

    private void OnOrderUpdated(OrderUpdatedEvent orderUpdatedEvent)
    {
        if (orderUpdatedEvent.Transform != _characterTransform)return;
        var listItem = orderUpdatedEvent.CurrentOrder;
        
        switch (listItem.Target.TypeInteractablePoint)
        {
            case TypeInteractablePoints.CashRegister:
                UpdateOrderView(listItem.Target.StandIcon);
                break;
            case TypeInteractablePoints.Exit:
                UpdateOrderView(listItem.Target.StandIcon);
                break;
            case TypeInteractablePoints.Stand:
                UpdateOrderView(listItem);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateOrderView(IOrder currentOrder)
    {
        _productIcon.sprite = currentOrder.Target.StandIcon;
        _productCount.text = currentOrder.CurrentCount + "/" + currentOrder.MaxCount;
    }
    private void UpdateOrderView(Sprite icon)
    {
        _productCount.text = "";
        _productIcon.sprite = icon;
    }
    
    public void UpdateOrderView(Sprite icon, int productsCount, int maxProductsCount)
    {
        _productCount.text = productsCount + "/" + maxProductsCount;
        _productIcon.sprite = icon;
    }
}