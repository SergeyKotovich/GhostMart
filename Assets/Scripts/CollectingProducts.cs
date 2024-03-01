using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectingProducts : MonoBehaviour
{
    private IWorker _character;
    [SerializeField] private Transform[] _allPositionsInBasket;

    [SerializeField] private Recycle _recycle;

    //[SerializeField] private GameObject _spawnerBonus;
    [SerializeField] private ProductConfig _productConfig;

    private void Awake()
    {
        _character = GetComponent<IWorker>();
    }

    private void Start()
    {
        _character.Basket.CountProductsChanged += UpdateCountProductsInBasket;
    }

    private int _currentIndexPositionInBasket;

    
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
    // public GameObject TryGetProduct()
    // {
    //     if (_listAllProductsInHands.Count == 0) return null;

    //     var product = _listAllProductsInHands[^1];
    //     _listAllProductsInHands.Remove(product);
    //     
    //     return product;
    // }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Recycle"))
    //    {
    //        _recycle.Recycling(_listAllProductsInHands);
    //        _listAllProductsInHands.Clear();
    //    }
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


