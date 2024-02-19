using System.Collections.Generic;
using Banana;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class CollectingProducts : MonoBehaviour
{
    public int CountAvailablePlaces => _maxProductsInHands - _listAllProductsInHands.Count;
    [SerializeField] private Transform[] _allPositionsInHands;
    [SerializeField] private Recycle _recycle;
    [SerializeField] private GameObject _spawnerBonus;
    [SerializeField] private ProductConfig  _productConfigCorn;
    [SerializeField] private ProductConfig  _productConfigBanana;

    private List<GameObject> _listAllProductsInHands = new();
    private int _maxProductsInHands = 3;

    public void PickUpProduct(GameObject product)
    {
        var currentIndexPoint = _listAllProductsInHands.Count;
        product.transform.SetParent(_allPositionsInHands[currentIndexPoint]);
        switch (product.tag)
        {
            case "Banana":
                SetBananaConfig(product);
                break;
            case "Corn": 
                SetCornConfig(product);
                break;
        }
        
        
        _listAllProductsInHands.Add(product.gameObject);
    }

    private void SetBananaConfig(GameObject product)
    {
        product.transform.DOLocalMove(_productConfigBanana.PositionProductInBasket, _productConfigBanana.SizeChangeTime);
        product.transform.DOLocalRotate(_productConfigBanana.RotationProductInBasket, _productConfigBanana.SizeChangeTime);
        product.transform.DOScale(_productConfigBanana.ScaleProductInbasket, _productConfigBanana.SizeChangeTime);
    }
    private void SetCornConfig(GameObject product)
    {
        product.transform.DOLocalMove(_productConfigCorn.PositionProductInBasket, _productConfigCorn.SizeChangeTime);
        product.transform.DOLocalRotate(_productConfigCorn.RotationProductInBasket, _productConfigCorn.SizeChangeTime);
        product.transform.DOScale(_productConfigCorn.ScaleProductInbasket, _productConfigCorn.SizeChangeTime);
    }

    private void OnTriggerStay(Collider other)
    {
        //  var currentParent = _listAllProductsInHands.Count;
          //  corn.transform.SetParent(_allPositionsInHands[currentParent]);
          //  corn.transform.DOLocalMove(new Vector3(0, 0, 0), 0.3f);
          //  corn.transform.DOLocalRotate(new Vector3(0, 270, 0), 0.3f);
          //  corn.transform.DOScale(new Vector3(0.002f, 0.002f, 0.002f), 0.3f);
          //  _listAllProductsInHands.Add(corn.gameObject);
          
        if (other.gameObject.CompareTag("Stand"))
        {
            var stand = other.gameObject.GetComponentInParent<Banana.Stand>();

            for (int i = 0; i < _listAllProductsInHands.Count; i++)
            {
                if (stand.SetProductOnStand(_listAllProductsInHands[i]))
                {
                    _listAllProductsInHands.RemoveAt(i);
                }
            }
        }

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