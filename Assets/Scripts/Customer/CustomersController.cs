using System;
using UnityEngine;

namespace Customer
{
    public class CustomersController : MonoBehaviour
    {
        [SerializeField] private int _maxCustomersCountInMart;
        [SerializeField] private CustomersSpawner _customersSpawner;
        private int _currentCustomersCountInMart;

        private void Awake()
        {
            _customersSpawner.StartSpawn(_maxCustomersCountInMart);
        }

        public void IncreaseMaxCustomersCount(int value)
        {
            _maxCustomersCountInMart += value;
        }

        public void OnCustomerLeft()
        {
            _currentCustomersCountInMart--;
            if (_currentCustomersCountInMart == 0)
            {
                _customersSpawner.StartSpawn(_maxCustomersCountInMart);
            }
        }
        public void OnCustomerSpawned()
        {
            _currentCustomersCountInMart++;
        }
    }
}