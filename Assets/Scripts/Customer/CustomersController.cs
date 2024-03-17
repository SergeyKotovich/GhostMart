using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Events;
using UnityEngine;

namespace Customer
{
    public class CustomersController : MonoBehaviour
    {
        [SerializeField] private int _maxCustomersCountInMart;
        [SerializeField] private int _delay;
        [SerializeField] private CustomersSpawner _customersSpawner;
        private int _currentCustomersCountInMart;
        private IDisposable _subscription;

        private void Awake()
        {
            _subscription = EventStreams.Global.Subscribe<CustomerLeftEvent>(OnCustomerLeft);
        }

        public void Initialize()
        {
            OnAllCustomersLeft();
        }
        public void IncreaseMaxCustomersCount(int value)
        {
            _maxCustomersCountInMart += value;
        }
        
        private void OnCustomerLeft(CustomerLeftEvent customerLeftEvent)
        {
            _currentCustomersCountInMart--;
            if (_currentCustomersCountInMart == 0)
            { 
                OnAllCustomersLeft();
            }
        }

        private async Task OnAllCustomersLeft()
        {
            while (_currentCustomersCountInMart < _maxCustomersCountInMart)
            {
                _customersSpawner.Spawn();
                _currentCustomersCountInMart++;
                await UniTask.Delay(_delay * 1000);
            }
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}