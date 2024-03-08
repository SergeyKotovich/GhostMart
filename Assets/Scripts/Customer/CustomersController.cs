using System;
using Events;
using UnityEngine;

namespace Customer
{
    public class CustomersController : MonoBehaviour
    {
        [SerializeField] private int _maxCustomersCountInMart;
        [SerializeField] private CustomersSpawner _customersSpawner;
        private int _currentCustomersCountInMart;
        private IDisposable _subscription;

        public void Initialize()
        {
            _customersSpawner.StartSpawn(_maxCustomersCountInMart);
            _subscription = EventStreams.Global.Subscribe<CustomerLeftEvent>(OnCustomerLeft);
        }

        public void IncreaseMaxCustomersCount(int value)
        {
            _maxCustomersCountInMart += value;
        }

        public void OnCustomerSpawned()
        {
            _currentCustomersCountInMart++;
        }
        private void OnCustomerLeft(CustomerLeftEvent customerLeftEvent)
        {
            _currentCustomersCountInMart--;
            if (_currentCustomersCountInMart == 0)
            {
                _customersSpawner.StartSpawn(_maxCustomersCountInMart);
            }
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }
    }
}