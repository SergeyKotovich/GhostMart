using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnerBonus : MonoBehaviour
{
    [SerializeField] private Bonus _objectToSpawn;
    [SerializeField] private Vector3 _targetPosition; // Vector3(-4.34656954,0,-48.4787254)
    [SerializeField] private float _minSpawnTime = 30f;
    [SerializeField] private float _maxSpawnTime = 120f;
    [SerializeField] private float _speed;

    private Bonus _currentBonus;
    
    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private void InitializeBonus(Bonus bonus)
    {
        _currentBonus = bonus;
        _currentBonus.BonusFlewToTarget += StartSpawning;
    }
    IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

        var spawnedObject = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
        InitializeBonus(spawnedObject);

        var newRotation = Quaternion.Euler(0f, 180f, 0f);
        spawnedObject.transform.rotation = newRotation;
        
        spawnedObject.transform.SetParent(transform);

        // движения объекта к целевой позиции
        StartCoroutine(MoveToObject(spawnedObject));

        // Ожидание, пока объект достигнет позиции
        yield return new WaitUntil(() => (spawnedObject.transform.position - _targetPosition).sqrMagnitude < 0.01f);

        // Включение анимации
        Animator objectAnimator = spawnedObject.GetComponent<Animator>();
        if (objectAnimator != null)
        {
            objectAnimator.Play("Eat");
        }
    }

    IEnumerator MoveToObject(Bonus obj)
    {
        Vector3 startPosition = obj.transform.position;
        float travelLength = Vector3.Distance(startPosition, _targetPosition);

        float startTime = Time.time;

        while (obj.transform.position != _targetPosition)
        {
            float distCovered = (Time.time - startTime) * _speed;
            float fractionOfTravel = distCovered / travelLength;
            obj.transform.position = Vector3.Lerp(startPosition, _targetPosition, fractionOfTravel);
            yield return null;
        }
    }
    private void StartSpawning()
    {
        StartCoroutine(SpawnObject());
    }

    private void OnDestroy()
    {
        _currentBonus.BonusFlewToTarget -= StartSpawning;
    }
}