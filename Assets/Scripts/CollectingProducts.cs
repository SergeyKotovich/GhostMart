using System.Collections.Generic;
using Banana;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class CollectingProducts : MonoBehaviour
{
    [SerializeField] private Transform[] _allPositionsInHands;
    [SerializeField] private Recycle _recycle;
    [SerializeField] private GameObject _spawnerBonus;
    [SerializeField] private ProductConfig _productConfig;

    private List<GameObject> _listAllProductsInHands = new();
    private int _maxProductsInHands = 3;
    private Stack<GameObject> _allAvailableBananas = new();
    private Stack<GameObject> _allAvailableCorn = new();

    public void TryPickUpProduct(GameObject product1)
    {
        if (_allAvailableBananas.Count != 0)
        {
            if (_listAllProductsInHands.Count >= _maxProductsInHands)
            {
                return;
            }

            var product = _allAvailableBananas.Pop();
            var currentParent = _listAllProductsInHands.Count;
            product.transform.SetParent(_allPositionsInHands[currentParent]);
            product.transform.DOLocalMove(_productConfig.PositionProductInBasket, _productConfig.SizeChangeTime);
            product.transform.DOLocalRotate(_productConfig.RotationProductInBasket, _productConfig.SizeChangeTime);
            product.transform.DOScale(_productConfig.ScaleProductInbasket, _productConfig.SizeChangeTime);
            _listAllProductsInHands.Add(product.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag(GlobalConstants.BANANA_PLANT_TAG))
        {
            
        }

        if (other.gameObject.CompareTag(GlobalConstants.CORN_PLANT_TAG))
        {
            if (_allAvailableCorn.Count != 0)
            {
                if (_listAllProductsInHands.Count >= _maxProductsInHands)
                {
                    return;
                }

                var corn = _allAvailableCorn.Pop();
                var currentParent = _listAllProductsInHands.Count;
                corn.transform.SetParent(_allPositionsInHands[currentParent]);
                corn.transform.DOLocalMove(new Vector3(0, 0, 0), 0.3f);
                corn.transform.DOLocalRotate(new Vector3(0, 270, 0), 0.3f);
                corn.transform.DOScale(new Vector3(0.002f, 0.002f, 0.002f), 0.3f);
                _listAllProductsInHands.Add(corn.gameObject);
            }
        }

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

    [UsedImplicitly]
    public void UpdateAvailableBananas(Stack<GameObject> allAvailableBananas)
    {
        _allAvailableBananas = allAvailableBananas;
    }

    [UsedImplicitly]
    public void UpdateAvailableCorn(Stack<GameObject> allAvailableCorn)
    {
        _allAvailableCorn = allAvailableCorn;
    }
   
}