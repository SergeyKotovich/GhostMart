using System;
using System.Collections;
using System.Collections.Generic;
using Customer;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace BadCustomer
{
    public class BadCustomer : MonoBehaviour
    {
        [SerializeField] private Stand[] _stands;
        [SerializeField] private StandSecondType[] _standsSecondType;

        [FormerlySerializedAs("_productBarSpawner")] [SerializeField] private OrderViewSpawner orderViewSpawner; // Тут надо добавить тучку со смайликом.
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


        public IInteractable GetRandomStand()
        {
            List<IInteractable> availableStands = new();
            for (var i = 0; i < _stands.Length; i++)
            {
                if (_stands[i].IsAvailable)
                {
                    availableStands.Add(_stands[i]);
                }
            }

            for (int i = 0; i < _standsSecondType.Length; i++)
            {
                if (_standsSecondType[i].IsAvailable)
                {
                    availableStands.Add(_standsSecondType[i]);
                }
            }
            
            var randomIndex = Random.Range(0, availableStands.Count);
            return availableStands[randomIndex];
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