using System;
using UnityEngine;
using UnityEngine.AI;

namespace MovementController
{
    public class MovementController : MonoBehaviour
    {
        [field:SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [SerializeField] private Animator _animator;
        private static readonly int _isMoving = Animator.StringToHash("IsMoving");

        private void Update()
        {
            _animator.SetBool(_isMoving, !IsAtTargetPoint());
        }
        public void SetDestination(Vector3 destination)
        {
            NavMeshAgent.SetDestination(destination);
            _animator.SetBool(_isMoving, true);
        }

        public bool IsAtTargetPoint()
        {
            return NavMeshAgent.remainingDistance < 1f;
        }

        public void IncreaseSpeed()
        {
            var currentSpeed = NavMeshAgent.speed;
            NavMeshAgent.speed = currentSpeed + 2;
            Debug.Log("speed = " + NavMeshAgent.speed);
        }
        
    }
}
