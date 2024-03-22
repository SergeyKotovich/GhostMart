using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Events;
using Order;
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
        private List<Customer> _currentCustomers = new();
        
        public void Spawn()
        {
            var randomIndex = Random.Range(0, _customersPrefabs.Length);
            var customer = Instantiate(_customersPrefabs[randomIndex]);

            var path = pathCreator.GetRandomPath();
            EventStreams.Global.Publish(new CharacterWasSpawnedEvent(customer.transform));

                
            customer.Initialize(path);
                
            customer.transform.position = _spawnPoint.position;
            _currentCustomers.Add(customer);
        }
    }
}