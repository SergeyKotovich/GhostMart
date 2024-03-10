using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace BadCustomer
{
    public class MoveToTargetState : MonoBehaviour, IState

    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private BadCustomer _badCustomer;
        [SerializeField] private ExitPoint _exit;

        private bool _cameToTarget;
        private IState _stateImplementation;
        private StateMachine _stateMachine;
        private bool _isActive;
        private IInteractable _currentPoint;

        private void Update()
        {
            if (!_isActive)
            {
                return;
            }

            if (_navMeshAgent.remainingDistance <= 1f)
            {
                _animator.SetBool("IsMoving", false);
                _cameToTarget = true;
                if (_cameToTarget)
                {
                    OnCameToTarget();
                }
            }
        }

        public void OnEnter()
        {
            if (_badCustomer._isMovingToExit)
            {
                _currentPoint = (IInteractable)_exit;
                MoveToTarget(_currentPoint.PointForCustomers.position);
            }
            else
            {
                _currentPoint = _badCustomer.GetRandomStand();
                MoveToTarget(_currentPoint.PointForCustomers.position);
            }
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void MoveToTarget(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
            _animator.SetBool("IsMoving", true);
            _isActive = true;
        }

        private void EnterDropProductState()
        {
            var curentPoint = (IStand)_currentPoint;
            _stateMachine.Enter<DropProductState, IStand>(curentPoint);
            _isActive = false;
        }

        private void OnCameToTarget()
        {
            if (_currentPoint.TypeInteractablePoint == TypeInteractablePoints.Exit)
            {
                EventStreams.Global.Publish(new CameToExitEvent());
            }

            if (_currentPoint.TypeInteractablePoint == TypeInteractablePoints.Stand)
            {
                EnterDropProductState();
            }
        }
    }
}