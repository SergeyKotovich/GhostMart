using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
//using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CollectingProducts : MonoBehaviour
{
    public UnityEvent ProductCollected;
    [SerializeField] private Transform [] _allPositions;
    private List<Transform> _listAllProducts = new();
    private int _maxProductsInHand = 3;
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag(GlobalConstants.CORN_TAG))
        {
            Debug.Log("Corn");
            if (_listAllProducts.Count>=_maxProductsInHand)
            {
                return;
            }
            var currentParent = _listAllProducts.Count; 
            other.transform.SetParent(_allPositions[currentParent]);
            other.transform.DOLocalMove(new Vector3(0,0,0), 0.3f);
            other.transform.DOLocalRotate(new Vector3(0,270,0),0.3f);
            other.transform.DOScale(new Vector3(0.002f, 0.002f, 0.002f),0.3f) ;
            _listAllProducts.Add(other.transform);
            
            ProductCollected?.Invoke();
        }

        if (other.gameObject.CompareTag(GlobalConstants.BANANA_TAG))
        {
            Debug.Log("banana");
            if (_listAllProducts.Count>=_maxProductsInHand)
            {
                return;
            }
            var currentParent = _listAllProducts.Count; 
            other.transform.SetParent(_allPositions[currentParent]);
            other.transform.DOLocalMove(new Vector3(0.002f,0,0), 0.3f);
            other.transform.DOLocalRotate(new Vector3(0,0,90),0.3f);
            other.transform.DOScale(new Vector3(0.013f, 0.013f, 0.013f),0.3f) ;
            _listAllProducts.Add(other.transform);
            ProductCollected?.Invoke();
        }
        
    }
}