using System;
using Customer;
using UnityEngine;
using DG.Tweening;
using Events;
using Interfaces;
using SparrowBonus;

public class Bonus : MonoBehaviour
{
    public event Action BonusFlewToTarget;
    [field: SerializeField] public BonusMovement BonusMovement { get; private set; }
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private Collider _collider;
    [SerializeField] private SparrowBonusConfig _sparrowBonusConfig;
    
    private IInteractable _landingPoint;
    private CurrentOrder _currentOrder;
    private bool _gotEnoughProducts;
    
    public void Initialize(IInteractable landingPoint)
    {
        _landingPoint = landingPoint;
        _currentOrder = new CurrentOrder(_landingPoint, 2);
        EventStreams.Global.Publish(new OrderUpdatedEvent(_currentOrder, transform));
    }

    public void GetBonus(Product product)
    {
        if (_gotEnoughProducts)
        {
            return;
        }

        if (product != null)
        {
            product.transform.SetParent(transform);
            product.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
            product.transform.DOScale(new Vector3(0, 0, 0), 0.5f);

            Destroy(product, 2f);
            
            _currentOrder.OnGotProduct();
            EventStreams.Global.Publish(new OrderUpdatedEvent(_currentOrder, transform));
        }

        if (_currentOrder.CurrentCount >= _currentOrder.MaxCount)
        {
            SwitcherStateTrigger(false);
            _gotEnoughProducts = true;
            
            var prefabRotation = _moneyPrefab.transform.rotation;

            var shift = 0;
            for (int i = 0; i < _sparrowBonusConfig.MaxMoneyReword; i++)
            {
                var bonus = Instantiate(_moneyPrefab, transform.position, prefabRotation);

                var x = bonus.transform.position.x;
                var z = bonus.transform.position.z;
                bonus.transform.DOLocalMove(new Vector3(x+shift, 0, z-shift), 0.5f);
                bonus.transform.DOScale(new Vector3(10, 10, 10), 0.5f);
                
                shift++;
            }
            
            EventStreams.Global.Publish(new CharacterDestroyedEvent(transform));
            BonusMovement.MoveToTarget(new Vector3(-4.26000023f, 22.3799992f, -110.699997f));
            BonusFlewToTarget?.Invoke();
            Destroy(gameObject, 21);
            _currentOrder.Reset();
        }
    }

    public void SwitcherStateTrigger(bool state)
    {
        _collider.isTrigger = state;
    }
    
}