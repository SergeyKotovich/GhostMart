using System;
using UnityEngine;
using UnityEngine.AI;

namespace MovementController
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        private static readonly int _isMoving = Animator.StringToHash("IsMoving");

        private void Update()
        {
            _animator.SetBool(_isMoving, !IsAtTargetPoint());
        }

        public void SetDestination(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
            _animator.SetBool(_isMoving, true);
        }

        public bool IsAtTargetPoint()
        {
            return _navMeshAgent.remainingDistance < 1f;
        }
        
    }
}
