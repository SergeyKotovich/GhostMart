using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomersSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _customersPrefabs;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _dellayBetweenSpawn;
        [SerializeField] private int _maxCustomersCount;


        private int _currentCustomersCount;

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

                customer.transform.position = _spawnPoint.position;

                _currentCustomersCount++;
                yield return new WaitForSeconds(_dellayBetweenSpawn);
            }

            yield return null;
        }
    }
}