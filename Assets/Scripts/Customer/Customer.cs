
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Customer
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private PathCreator _pathCreator;
        
        private int _currentPathIndex;
        private Vector3[] _path;

        private void Start()
        {
            // TODO: need to get rid of FindAnyObjectByType
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