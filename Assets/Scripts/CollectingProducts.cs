using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Banana;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class CollectingProducts : MonoBehaviour
{
    [SerializeField] private Transform [] _allPositionsInHands;
    private List<GameObject> _listAllProductsInHands = new();
    private int _maxProductsInHands = 3;
    [SerializeField] private ProductsStander _productsStander;
    private Stack<GameObject> _allAvailableBananas = new();
    private Stack<GameObject> _allAvailableCorn = new();
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.BANANA_PLANT_TAG))
        {
            if (_allAvailableBananas.Count!=0)
            {
                if (_listAllProductsInHands.Count>=_maxProductsInHands)
                {
                    return;
                }
                var banana = _allAvailableBananas.Pop();
                var currentParent = _listAllProductsInHands.Count; 
                banana.transform.SetParent(_allPositionsInHands[currentParent]);
                banana.transform.DOLocalMove(new Vector3(0.002f,0,0), 0.3f);
                banana.transform.DOLocalRotate(new Vector3(0,0,90),0.3f);
                banana.transform.DOScale(new Vector3(0.013f, 0.013f, 0.013f),0.3f) ;
                _listAllProductsInHands.Add(banana.gameObject);
            }
        }
        
        if (other.gameObject.CompareTag(GlobalConstants.CORN_PLANT_TAG))
        {
            if (_allAvailableCorn.Count!=0)
            {
                if (_listAllProductsInHands.Count>=_maxProductsInHands)
                {
                    return;
                }
                var corn = _allAvailableCorn.Pop();
                var currentParent = _listAllProductsInHands.Count; 
                corn.transform.SetParent(_allPositionsInHands[currentParent]);
                corn.transform.DOLocalMove(new Vector3(0,0,0), 0.3f);
                corn.transform.DOLocalRotate(new Vector3(0,270,0),0.3f);
                corn.transform.DOScale(new Vector3(0.002f, 0.002f, 0.002f),0.3f) ;
                _listAllProductsInHands.Add(corn.gameObject);
            }
        }
        
        if (other.gameObject.CompareTag("Stand"))
        {
            //_productsStander.TrySetProducts(_listAllProducts);
            var stand = other.gameObject.GetComponentInParent<Stand>();


            for (int i = 0; i < _listAllProductsInHands.Count; i++)
            {
                if (stand.SetProductOnStand(_listAllProductsInHands[i]))
                {
                    _listAllProductsInHands.RemoveAt(i);
                }
            }
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