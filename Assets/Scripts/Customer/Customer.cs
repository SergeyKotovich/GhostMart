using System;
using System.Collections.Generic;
using DefaultNamespace.Banana;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Customer
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;

        private void Start()
        {
            // TODO: replace by using tire

            var pathCreator = FindAnyObjectByType<PathCreator>();
            var randomPath = pathCreator.GetRandomPath();
            
            GoToTargets(randomPath);
        }

        private void GoToTargets(Vector3[] path)
        {
            
            transform.DOPath(path, 1f / _movementSpeed)
                .SetSpeedBased()
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Debug.Log("На Месте!");
                });
        }
        

    }
}