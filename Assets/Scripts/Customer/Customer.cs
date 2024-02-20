
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Customer
{
    public class Customer : MonoBehaviour
    { 
        public event Action CameToTarget;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;

        private int _currentPathIndex;
        private bool _isMoving;
        public Vector3[] Path { get; private set; }

        public void Initialize(Vector3[] path)
        {
            Path = path;
        }

        void Update()
        {
            if (_navMeshAgent.remainingDistance < 1f && !_navMeshAgent.pathPending)
            {
                CameToTarget?.Invoke();
            }
        }

        public void MoveToNextPoint()
        {
            
            if (_currentPathIndex < Path.Length)
            {
                _navMeshAgent.SetDestination(Path[_currentPathIndex]);
                _currentPathIndex++;
                _isMoving = true;
                _animator.SetBool("IsMoving", _isMoving);
            }
            else
            {
                _navMeshAgent.isStopped = true;
                _animator.SetBool("IsMoving", false);
            }
        }

        public void StopMoving()
        {
            _isMoving = false;
            _animator.SetBool("IsMoving", _isMoving);
        }
        
    }
}