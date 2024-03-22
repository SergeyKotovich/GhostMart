
using System;
using Customer;
using Events;
using SimpleEventBus.Disposables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Order
{
    public class OrderView : MonoBehaviour
    {

        [SerializeField] private Image _productIcon;
        [SerializeField] private TextMeshProUGUI _productCount;
        private Transform _characterTransform;
        private readonly CompositeDisposable _subscriptions = new();

        private void Awake()
        {
            _subscriptions.Add(EventStreams.Global.Subscribe<OrderUpdatedEvent>(OnOrderUpdated));
            _subscriptions.Add(EventStreams.Global.Subscribe<CharacterDestroyedEvent>(OnCharacterDestroyed));
        }

        public void Initialize(Transform transform)
        {
            _characterTransform = transform;
        }

        private void OnOrderUpdated(OrderUpdatedEvent orderUpdatedEvent)
        {
            if (orderUpdatedEvent.Transform != _characterTransform) return;
            var listItem = orderUpdatedEvent.CurrentOrder;

            switch (listItem.Target.Type)
            {
                case InteractableTypes.CashRegister or InteractableTypes.Exit:
                    UpdateOrderView(listItem.Target.Icon);
                    break;
                case InteractableTypes.Stand:
                    UpdateOrderView(listItem);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateOrderView(IOrder currentOrder)
        {
            _productIcon.sprite = currentOrder.Target.Icon;
            _productCount.text = currentOrder.CurrentCount + "/" + currentOrder.MaxCount;
        }

        private void UpdateOrderView(Sprite icon)
        {
            _productCount.text = "";
            _productIcon.sprite = icon;
        }

        private void OnCharacterDestroyed(CharacterDestroyedEvent characterDestroyedEvent)
        {
            if (characterDestroyedEvent.Transform != _characterTransform) return;

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}