using System;
using Customer;
using UnityEngine;
using DG.Tweening;
using Events;
using Interfaces;

namespace SparrowBonus
{
    public class Bonus : MonoBehaviour
    {
        public event Action BonusFlewToExit;
        [field: SerializeField] public BonusMovement BonusMovement { get; private set; }
        [SerializeField] private MoneySpawner _moneySpawner;
        [SerializeField] private SparrowBonusConfig _sparrowBonusConfig;
        [SerializeField] private Collider _collider;

        private IInteractable _landingPoint;
        private CurrentOrder _currentOrder;
        private bool _isOrderCompleted;

        public void Initialize(IInteractable landingPoint)
        {
            _landingPoint = landingPoint;
            _currentOrder = new CurrentOrder(_landingPoint, _sparrowBonusConfig.MaxProductsCount);
        }

        public void GetBonus(Product product)
        {
            if (_isOrderCompleted) return;
            TakeProduct(product);

            if (_currentOrder.CurrentCount < _currentOrder.MaxCount) return;
            OnOrderCompleted();
            FlyAway();
        }

        public void OnOrderUpdated()
        {
            EventStreams.Global.Publish(new OrderUpdatedEvent(_currentOrder, transform));
        }

        private void FlyAway()
        {
            EventStreams.Global.Publish(new CharacterDestroyedEvent(transform));
            BonusMovement.MoveToTarget(_sparrowBonusConfig.ExitPosition, OnMovementCompleted);
        }

        private void OnMovementCompleted()
        {
            BonusFlewToExit?.Invoke();
            Destroy(gameObject);
        }

        private void OnOrderCompleted()
        {
            _isOrderCompleted = true;
            SetTriggerState(false);

            _moneySpawner.Spawn();
        }

        private void TakeProduct(Product product)
        {
            if (product == null) return;

            product.transform.SetParent(transform);
            product.transform.DOLocalMove(Vector3.zero, _sparrowBonusConfig.AnimationDuration);
            product.transform.DOScale(Vector3.zero, _sparrowBonusConfig.AnimationDuration).OnComplete(() => Destroy(product));

            _currentOrder.OnGotProduct();
            OnOrderUpdated();
        }

        public void SetTriggerState(bool value)
        {
            _collider.isTrigger = value;
        }

    }
}