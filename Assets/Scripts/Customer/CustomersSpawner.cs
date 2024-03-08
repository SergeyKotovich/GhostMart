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
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private ProductBarSpawner _productBarSpawner;
        [SerializeField] private CustomersController _customerController;
        private List<Customer> _currentCustomers = new();
        
        public void StartSpawn(int maxCustomersCount)
        {
            StartCoroutine(SpawnCustomers(maxCustomersCount));
        }
        
        private IEnumerator SpawnCustomers(int maxCustomersCount)
        {
            var currentCustomersCount = 0;
            while (currentCustomersCount < maxCustomersCount)
            {
                var randomIndex = Random.Range(0, _customersPrefabs.Length);
                var customer = Instantiate(_customersPrefabs[randomIndex]);

                var path = _pathCreator.GetRandomPath();
                var productBar = _productBarSpawner.GetProductBar(customer.gameObject);
                
                customer.Initialize(path, productBar);
                
                customer.transform.position = _spawnPoint.position;
                _currentCustomers.Add(customer);

                currentCustomersCount++;
                _customerController.OnCustomerSpawned();
                yield return new WaitForSeconds(_dellayBetweenSpawn);
            }
            
            yield return null;
        }
    }
}