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
        
        private int _currentCustomersCount;
        private List<Customer> _currentCustomers = new List<Customer>();

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
                Debug.Log("CustomersSpawner");
                customer.Initialize(_pathCreator.GetRandomPath());
                customer.transform.position = _spawnPoint.position;
                _currentCustomers.Add(customer);

                _currentCustomersCount++;
                yield return new WaitForSeconds(_dellayBetweenSpawn);
            }

            yield return null;
        }
    }
}