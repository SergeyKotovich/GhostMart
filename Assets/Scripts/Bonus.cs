using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Bonus : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField]private int _maxMoney=3;
    
    private List<GameObject> _listAllProductsInHands;
    private int _productCounter;
    

    public void GetBonus()
    {
        for (int i = _listAllProductsInHands.Count - 1; i >= 0; i--)
        {
            if (_productCounter >= 2)
            {
                break;
            }

            var product = _listAllProductsInHands[i];
            if (product.CompareTag("Corn") && _productCounter <= 2)
            {
                product.transform.SetParent(transform);
                product.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
                product.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
                _listAllProductsInHands.RemoveAt(i);
                Destroy(product, 2f);
                _productCounter++;
            }
        }

        if (_productCounter >= 2)
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
            
            var bonusObject = gameObject.GetComponentInParent<SpawnerBonus>();
            var animator = GetComponent<Animator>();

            var prefabRotation = _gameObjectPrefab.transform.rotation;

            var shift = 0;
            for (int i = 0; i < _maxMoney; i++)
            {
                var bonus = Instantiate(_gameObjectPrefab, transform.position, prefabRotation);

                bonus.transform.DOLocalMove(new Vector3(-1+shift, 0, -50-shift), 0.5f);
                bonus.transform.DOScale(new Vector3(10, 10, 10), 0.5f);

                animator.Play("Fly");
                transform.DOMove(new Vector3(-4.26000023f, 22.3799992f, -110.699997f), 15f);
                shift++;
            }
 

            _productCounter = 0;

            Destroy(gameObject, 16f);
            bonusObject.StartSpawning();
            
        }
    }

    public void GetProductList(List<GameObject> other)
    {
        _listAllProductsInHands = other;
    }
}