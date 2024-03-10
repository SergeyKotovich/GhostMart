using System;
using System.Collections;
using System.Collections.Generic;
using Customer;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace BadCustomer
{
    public class BadCustomer : MonoBehaviour
    {
        [SerializeField] private Stand[] _stands;

        [SerializeField] private ProductBarSpawner _productBarSpawner; // Тут надо добавить тучку со смайликом.
        [field: SerializeField] public Collider _collider { get; private set; }

        public bool _isMovingToExit{ get; private set; }

        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine
            (
                GetComponent<MoveToTargetState>(),
                GetComponent<DropProductState>(),
                GetComponent<WaitingState>()
            );

            _stateMachine.Initialize();
            _stateMachine.Enter<MoveToTargetState>();
        }


        public Stand GetRandomStand()
        {
            var randomIndex = Random.Range(0, _stands.Length);
            return _stands[randomIndex];
        }

      
        public void SwitcherMovingToExit()
        {
            _isMovingToExit = !_isMovingToExit;
        }

        public void StateMachineStartMoving()
        {
            _stateMachine.Enter<MoveToTargetState>();
        }
    }
}