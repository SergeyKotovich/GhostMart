using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SimpleEventBus.Disposables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BadCustomer
{
    public class BadCustomerSpawner : MonoBehaviour
    {
        [SerializeField] private BadCustomer _badCustomer;
        [SerializeField] private OrderViewSpawner _orderViewSpawner;
        [SerializeField] private int _minSpawnTime;
        [SerializeField] private int _maxSpawnTime;
        
        private readonly CompositeDisposable _subscribers = new();

        private void Start()
        {
            _subscribers.Add(EventStreams.Global.Subscribe<CameToExitEvent>(OnCameToExit));
        }

        public void Initialize()
        {
            Spawn();
            _orderViewSpawner.Spawn(_badCustomer.transform);
        }
        private void OnCameToExit(CameToExitEvent cameToExitEvent)
        {
            _badCustomer.gameObject.SetActive(false);
            Spawn();
        }
        private async UniTask Spawn()
        {
            var randomDelay = Random.Range(_minSpawnTime, _maxSpawnTime);
            await UniTask.Delay(randomDelay);
            
            _badCustomer.gameObject.SetActive(true);
            _badCustomer.Reset();
        }

        private void OnDestroy()
        {
            _subscribers.Dispose();
        }
    }
}