using System.Collections.Generic;
using Banana;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class CollectingProducts : MonoBehaviour
{
    public int CountAvailablePlaces => _maxProductsInHands - _listAllProductsInHands.Count;
    [SerializeField] private Transform[] _allPositionsInHands;
    [SerializeField] private Recycle _recycle;
    [SerializeField] private GameObject _spawnerBonus;
    [SerializeField] private ProductConfig  _productConfig;

    private List<GameObject> _listAllProductsInHands = new();
    private int _maxProductsInHands = 3;

    public void PickUpProduct(GameObject product)
    {
        var currentIndexPoint = _listAllProductsInHands.Count;
        product.transform.SetParent(_allPositionsInHands[currentIndexPoint]);
        
        product.transform.DOLocalMove(_productConfig.PositionProductInBasket, _productConfig.SizeChangeTime);
        product.transform.DOLocalRotate(_productConfig.RotationProductInBasket, _productConfig.SizeChangeTime);
        product.transform.DOScale(_productConfig.ScaleProductInbasket, _productConfig.SizeChangeTime);

        _listAllProductsInHands.Add(product.gameObject);
    }

    public GameObject TryGetProduct()
    {
        if (_listAllProductsInHands.Count == 0) return null;

        var product = _listAllProductsInHands[^1];
        _listAllProductsInHands.Remove(product);
        
        return product;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Recycle"))
        {
            _recycle.Recycling(_listAllProductsInHands);
            _listAllProductsInHands.Clear();
        }

        if (other.gameObject.CompareTag("Bonus"))
        {
            var bonusObject = _spawnerBonus.GetComponent<SpawnerBonus>();
            var bonus = bonusObject.GetBonusObject();
            var getBonus = bonus.GetComponent<Bonus>();
            getBonus.GetProductList(_listAllProductsInHands);
            getBonus.GetBonus();
        }
    }
}