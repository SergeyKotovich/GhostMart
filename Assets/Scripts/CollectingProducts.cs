using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectingProducts : MonoBehaviour
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private Vector3 _shiftForNextProduct;
    [SerializeField] private ProductConfig _productConfig;

    private IWorker _character;
    private int _currentIndexPositionInBasket;
    private void Awake()
    {
        _character = GetComponent<IWorker>();
    }

    private void Start()
    {
        _character.Basket.CountProductsChanged += UpdateCountProductsInBasket;
    }
    
    public void SetPosition(Product product)
    {
        var currentPosition = Vector3.zero;
        if (_currentIndexPositionInBasket > 0)
        {
            currentPosition = _shiftForNextProduct * _currentIndexPositionInBasket;
        }
        
        product.transform.SetParent(_rootTransform);
        product.transform.DOLocalMove(currentPosition, _productConfig.SizeChangeTime);
        product.transform.DOLocalRotate(_productConfig.RotationProductInBasket, _productConfig.SizeChangeTime);
        product.transform.DOScale(_productConfig.ScaleProductInbasket, _productConfig.SizeChangeTime);
    }

    private void UpdateCountProductsInBasket(int currentIndexPositionInBasket)
    {
        _currentIndexPositionInBasket = currentIndexPositionInBasket;
    }
    private void OnDestroy()
    {
        _character.Basket.CountProductsChanged -= UpdateCountProductsInBasket;
    }
  
}


