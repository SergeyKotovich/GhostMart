using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bonus : MonoBehaviour
{
    public event Action BonusFlewToTarget;
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField] private int _maxMoney=3;
    [field: SerializeField] public BonusMovement BonusMovement { get; private set; }
    [SerializeField] private Collider _collider;

    private int _productCounter;
    private bool _gotEnoughProducts;
    private Bonus _bonus;
    
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
            _productCounter++;
        }

        if (_productCounter >= 2)
        {
            SwitcherStateTrigger(false);
            _gotEnoughProducts = true;
            
            var prefabRotation = _gameObjectPrefab.transform.rotation;

            var shift = 0;
            for (int i = 0; i < _maxMoney; i++)
            {
                var bonus = Instantiate(_gameObjectPrefab, transform.position, prefabRotation);

                bonus.transform.DOLocalMove(new Vector3(-1+shift, 0, -50-shift), 0.5f);
                bonus.transform.DOScale(new Vector3(10, 10, 10), 0.5f);
                
                shift++;
            }
            
            BonusMovement.MoveToTarget(new Vector3(-4.26000023f, 22.3799992f, -110.699997f));
            BonusFlewToTarget?.Invoke();
            Destroy(gameObject, 16);
            
            _productCounter = 0;
        }
    }

    public void SwitcherStateTrigger(bool state)
    {
        _collider.isTrigger = state;

    }
    
}