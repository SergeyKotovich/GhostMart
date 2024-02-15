using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class CollectingProducts : MonoBehaviour
{
    [SerializeField] private Transform [] _allPositions;
    private List<GameObject> _listAllProducts = new();
    private int _maxProductsInHand = 3;
    [SerializeField] private ProductsStander _productsStander;
    private Stack<GameObject> _allAvailableBananas = new();

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag(GlobalConstants.CORN_TAG))
        {
           
            if (_listAllProducts.Count>=_maxProductsInHand)
            {
                return;
            }
            var currentParent = _listAllProducts.Count; 
            other.transform.SetParent(_allPositions[currentParent]);
            other.transform.DOLocalMove(new Vector3(0,0,0), 0.3f);
            other.transform.DOLocalRotate(new Vector3(0,270,0),0.3f);
            other.transform.DOScale(new Vector3(0.002f, 0.002f, 0.002f),0.3f) ;
            _listAllProducts.Add(other.gameObject);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.BANANA_PLANTS_TAG))
        {
            if (_allAvailableBananas.Count!=0)
            {
                var banana = _allAvailableBananas.Pop();
                if (_listAllProducts.Count>=_maxProductsInHand)
                {
                    return;
                }
                var currentParent = _listAllProducts.Count; 
                banana.transform.SetParent(_allPositions[currentParent]);
                banana.transform.DOLocalMove(new Vector3(0.002f,0,0), 0.3f);
                banana.transform.DOLocalRotate(new Vector3(0,0,90),0.3f);
                banana.transform.DOScale(new Vector3(0.013f, 0.013f, 0.013f),0.3f) ;
                _listAllProducts.Add(banana.gameObject);
            }
            
        }
        
        if (other.gameObject.CompareTag("BananasStand"))
        {
            _productsStander.TrySetProducts(_listAllProducts);
            _listAllProducts.Clear();
        }
    }
    [UsedImplicitly]
    public void UpdateCountBananas(Stack<GameObject> allAvailableBananas)
    {
        _allAvailableBananas = allAvailableBananas;
    }
}