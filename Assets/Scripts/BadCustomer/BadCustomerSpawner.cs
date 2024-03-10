using System;
using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Disposables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BadCustomer
{
    public class BadCustomerSpawner : MonoBehaviour
    {
        [SerializeField] private BadCustomer _badCustomer;

        //private BadCustomer _badCustomer;

        [SerializeField] private float _minSpawnTime = 30f;
        [SerializeField] private float _maxSpawnTime = 120f;

        private GameObject _instantiateGameObject;
        private CompositeDisposable _subscribers = new();

        private void Start()
        {
            StartCoroutine(SpawnObject());
            _subscribers.Add(EventStreams.Global.Subscribe<CameToExitEvent>(StartSpawn));
        }

        private IEnumerator SpawnObject()
        {
            //yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));
            yield return new WaitForSeconds(1);

            _badCustomer.gameObject.SetActive(true);
            _badCustomer.StateMachineStartMoving();
        }

        private void StartSpawn(CameToExitEvent cameToExitEvent)
        {
            _badCustomer.SwitcherMovingToExit();
            _badCustomer.gameObject.SetActive(false);
            StartCoroutine(SpawnObject());
        }

        private void OnDestroy()
        {
            _subscribers.Dispose();
        }
    }
}