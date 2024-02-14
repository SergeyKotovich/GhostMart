using System;
using UnityEngine;

public interface ISpawner 
{
    float _currentTime { get; set; }
    float _delayBetweenSpawnObjects { get; set; }

    private void ObjectsSpawn()
    {
    }

}