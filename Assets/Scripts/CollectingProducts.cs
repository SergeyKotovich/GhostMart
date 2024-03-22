using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectingProducts : MonoBehaviour
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private Vector3 _shiftForNextProduct; 
    [SerializeField] private ProductConfig _productConfigFirstType;
    [SerializeField] private ProductConfig _productConfigSecondType;

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
    
    public void TryToSetPosition(Product product)
    {
        var currentPosition = Vector3.zero;
        if (_currentIndexPositionInBasket > 0)
        {
            currentPosition = _shiftForNextProduct * _currentIndexPositionInBasket;
        }

        if (product.Type == TypeProduct.Banana || product.Type == TypeProduct.Corn || product.Type == TypeProduct.Egg)
        {
            SetPosition(product, currentPosition, _productConfigFirstType);
        }
        else
        {
            SetPosition(product,currentPosition, _productConfigSecondType);
        }
        
        
    }

    private void SetPosition(Product product,Vector3 currentPosition, ProductConfig currenConfig)
    {
        product.transform.SetParent(_rootTransform);
        product.transform.DOLocalMove(currentPosition, currenConfig.SizeChangeTime);
        product.transform.DOLocalRotate(currenConfig.RotationProductInBasket, currenConfig.SizeChangeTime);
        
        product.transform.DOScale(currenConfig.ScaleProductInbasket, currenConfig.SizeChangeTime);
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


