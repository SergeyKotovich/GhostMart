using System;
using System.Collections.Generic;
using DefaultNamespace.Banana;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Customer
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        
        private int _currentPathIndex;
        private Vector3[] _path;

        private void Start()
        {
            // TODO: replace by using tire

            var pathCreator = FindAnyObjectByType<PathCreator>();
            _path = pathCreator.GetRandomPath();
            MoveToNextPoint();
        }
        
        void Update()
        {
            if (_navMeshAgent.remainingDistance < 1f && !_navMeshAgent.pathPending)
            {
                MoveToNextPoint();
            }
        }
        
        void MoveToNextPoint()
        {
            if (_currentPathIndex < _path.Length)
            {
                _navMeshAgent.SetDestination(_path[_currentPathIndex]);
                _currentPathIndex++;
                _animator.SetBool("IsMoving", true);
            }
            else
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("IsMoving", false);
            }
        }
        

    }
}