using UnityEngine;
using Random = UnityEngine.Random;

namespace Customer
{
    public class CustomersSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _customersPrefabs;
        [SerializeField] private Transform _spawnPoint;

        private void Awake()
        {
            // TODO: temp code

            var randomIndex = Random.Range(0, _customersPrefabs.Length);
            var customer = Instantiate(_customersPrefabs[randomIndex]);

            customer.transform.position = _spawnPoint.position;
        }
    }
}