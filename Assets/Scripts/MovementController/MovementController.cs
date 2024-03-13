using System;
using UnityEngine;
using UnityEngine.AI;

namespace MovementController
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;

        public void SetDestination(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
            _animator.SetBool("IsMoving", true);
        }

        public bool IsAtTargetPoint()
        {
            return _navMeshAgent.remainingDistance < 1f;
        }

        public void StopMoving()
        {
            _animator.SetBool("IsMoving", false);
        }
    }
}
