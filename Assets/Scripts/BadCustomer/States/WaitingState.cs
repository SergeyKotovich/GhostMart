using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BadCustomer
{
    public class WaitingState : MonoBehaviour, IPayLoadedState<IStand>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private BadCustomer _badCustomer;
        private StateMachine _stateMachine;
        private IStand _stand;
        private bool _isActive;
        private bool _trigger;

        private void Update()
        {
            if (_isActive && _stand.IsEmpty() == false)
            {
                _stateMachine.Enter<DropProductState, IStand>(_stand);
                _isActive = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG))
            {
                _badCustomer.SwitcherMovingToExit();
                _animator.SetBool("IsMoving", true);
                _stateMachine.Enter<MoveToTargetState>();
                _trigger = true;
                _isActive = false;
                _badCustomer._collider.isTrigger = false;
            }
        }

        public void OnEnter(IStand stand)
        {
            _stand = stand;
            _isActive = true;
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}