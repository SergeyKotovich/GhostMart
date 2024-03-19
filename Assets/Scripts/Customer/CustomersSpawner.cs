using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomersSpawner : MonoBehaviour
    {
        [SerializeField] private Customer[] _customersPrefabs;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PathCreator pathCreator;
        [SerializeField] private OrderViewSpawner orderViewSpawner;
        private List<Customer> _currentCustomers = new();
        
        public void Spawn()
        {
            var randomIndex = Random.Range(0, _customersPrefabs.Length);
            var customer = Instantiate(_customersPrefabs[randomIndex]);

            var path = pathCreator.GetRandomPath();
            orderViewSpawner.Spawn(customer.transform);
                
            customer.Initialize(path);
                
            customer.transform.position = _spawnPoint.position;
            _currentCustomers.Add(customer);
        }
    }
}