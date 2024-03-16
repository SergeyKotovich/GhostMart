using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;
    void Start()
    {
        SpawnObject();
    }

    private async UniTask SpawnObject()
    {
        var random = Random.Range(1000, 3001);
        foreach (var gameObject in _gameObjects)
        {
            await UniTask.Delay(random);
            gameObject.SetActive(true);
        }
    } 
    
}
