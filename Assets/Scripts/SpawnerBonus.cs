using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnerBonus : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Vector3 _targetPosition; // Vector3(-4.34656954,0,-48.4787254)
    [SerializeField] private float _minSpawnTime = 30f;
    [SerializeField] private float _maxSpawnTime = 120f;
    [SerializeField] private float _speed;

    private GameObject _spawnedObject;

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));

        GameObject spawnedObject = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);

        var newRotation = Quaternion.Euler(0f, 180f, 0f);
        spawnedObject.transform.rotation = newRotation;
        _spawnedObject = spawnedObject;
        
        spawnedObject.transform.SetParent(transform);

        // движения объекта к целевой позиции
        StartCoroutine(MoveToObject(spawnedObject, _speed));

        // Ожидание, пока объект достигнет позиции
        yield return new WaitUntil(() => (spawnedObject.transform.position - _targetPosition).sqrMagnitude < 0.01f);

        // Включение анимации
        Animator objectAnimator = spawnedObject.GetComponent<Animator>();
        if (objectAnimator != null)
        {
            objectAnimator.Play("Eat");
        }
    }

    IEnumerator MoveToObject(GameObject obj, float speed)
    {
        Vector3 startPosition = obj.transform.position;
        float travelLength = Vector3.Distance(startPosition, _targetPosition);

        float startTime = Time.time;
        float travelDuration = Vector3.Distance(startPosition, _targetPosition) / speed;

        while (obj.transform.position != _targetPosition)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfTravel = distCovered / travelLength;
            obj.transform.position = Vector3.Lerp(startPosition, _targetPosition, fractionOfTravel);
            yield return null;
        }
    }

    public GameObject GetBonusObject()
    {
        return _spawnedObject;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnObject());
    }
}