using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectingProducts : MonoBehaviour
{
    public UnityEvent ProductCollected;
    [SerializeField] private Transform _rootProduct;
    // private List<Transform> _listEmptyPositions = new();
    [SerializeField] private List<Transform> _listAllPositions = new();
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag(GlobalConstants.CORN_TAG) /*&& other.gameObject.CompareTag(GlobalConstants.BANANA_TAG*/)
        {
            Debug.Log("Corn");
            other.transform.SetParent(_rootProduct , false);
             other.transform.localPosition = Vector3.zero;
             other.transform.localRotation = Quaternion.Euler(0,270,0);
             other.transform.localScale = new Vector3(0.002f, 0.002f, 0.002f);
            
            ProductCollected?.Invoke();
        }

        if (other.gameObject.CompareTag(GlobalConstants.BANANA_TAG))
        {
            Debug.Log("banana");
            other.transform.SetParent(_rootProduct , false);
            other.transform.localPosition = new Vector3(-0.0021f, 0, 0);
            other.transform.localRotation = Quaternion.Euler(0, 0, 90);
            other.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        }
        ProductCollected?.Invoke();
    }
}