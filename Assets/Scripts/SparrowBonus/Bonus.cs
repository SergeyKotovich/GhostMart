using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SparrowBonus;

public class Bonus : MonoBehaviour
{
    public event Action BonusFlewToTarget;
    [field: SerializeField] public BonusMovement BonusMovement { get; private set; }
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField] private Collider _collider;
    [SerializeField] private SparrowBonusConfig _sparrowBonusConfig;

    private ProductBarView _productBarView;
    private int _productCounter;
    private bool _gotEnoughProducts;

    public void Initialize(ProductBarView productBarView)
    {
        _productBarView = productBarView;
        _productBarView.UpdateProductBar
            (_sparrowBonusConfig.TargetProductIcon, _productCounter, _sparrowBonusConfig.MaxProductsCount);
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
            _productCounter++;
            _productBarView.UpdateProductBar
                (_sparrowBonusConfig.TargetProductIcon, _productCounter, _sparrowBonusConfig.MaxProductsCount);        }

        if (_productCounter >= 2)
        {
            SwitcherStateTrigger(false);
            _gotEnoughProducts = true;
            
            var prefabRotation = _gameObjectPrefab.transform.rotation;

            var shift = 0;
            for (int i = 0; i < _sparrowBonusConfig.MaxMoneyReword; i++)
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