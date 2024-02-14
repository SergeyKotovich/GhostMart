using System;
using UnityEngine;

public interface IObjectsSpawner 
{
    float _currentTime { get; set; }
    float _delayBetweenSpawnObjects { get; set; } 

    public void ObjectsSpawn();

}