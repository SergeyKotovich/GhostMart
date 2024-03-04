using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectingProducts : MonoBehaviour
{
    [SerializeField] private Transform[] _allPositionsInBasket;
    //[SerializeField] private GameObject _spawnerBonus;
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
        product.transform.SetParent(_allPositionsInBasket[_currentIndexPositionInBasket]);

        product.transform.DOLocalMove(_productConfig.PositionProductInBasket, _productConfig.SizeChangeTime);
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
    
//
    // if (other.gameObject.CompareTag("Bonus"))
    // {
    //     var bonusObject = _spawnerBonus.GetComponent<SpawnerBonus>();
    //     var bonus = bonusObject.GetBonusObject();
    //     var getBonus = bonus.GetComponent<Bonus>();
    //     getBonus.GetProductList(_listAllProductsInHands);
    //     getBonus.GetBonus();
    // }
}


