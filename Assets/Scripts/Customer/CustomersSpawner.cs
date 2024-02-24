using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomersSpawner : MonoBehaviour
    {
        [SerializeField] private Customer[] _customersPrefabs;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _dellayBetweenSpawn;
        [SerializeField] private int _maxCustomersCount;
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private ProductBarSpawner _productBarSpawner;
        private int _currentCustomersCount;
        private List<Customer> _currentCustomers = new();

        private void Awake()
        {
            StartCoroutine(SpawnCustomers());
        }

        private IEnumerator SpawnCustomers()
        {
            while (_currentCustomersCount < _maxCustomersCount)
            {
                var randomIndex = Random.Range(0, _customersPrefabs.Length);
                var customer = Instantiate(_customersPrefabs[randomIndex]);

                var path = _pathCreator.GetRandomPath();
                var productBar = _productBarSpawner.GetProductBar(customer.gameObject);
                
                customer.Initialize(path, productBar);
                
                customer.transform.position = _spawnPoint.position;
                _currentCustomers.Add(customer);

                _currentCustomersCount++;
                yield return new WaitForSeconds(_dellayBetweenSpawn);
            }

            //TODO: нужен ли здесь yield return null; если да то для чего
            yield return null;
        }
    }
}