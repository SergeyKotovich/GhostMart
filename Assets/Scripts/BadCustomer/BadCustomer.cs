using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace BadCustomer
{
    public class BadCustomer : MonoBehaviour, IBadCustomer
    {
        [SerializeField] private MovementController.MovementController _movementController;
        [SerializeField] private PathCreator _pathCreator;
        public List<IInteractable> Path { get; private set; }
        private StateMachine _stateMachine;

        private void Awake()
        {
            Path = _pathCreator.GetRandomPath();
            _stateMachine = new StateMachine
            (
                GetComponent<MoveToTargetState>(),
                GetComponent<DropProductState>()
            );

            _stateMachine.Initialize();
        }
        
        public void SetDestination(Vector3 position)
        {
            _movementController.SetDestination(position);
        }

        public bool IsAtTargetPoint()
        {
            return _movementController.IsAtTargetPoint();
        }

        public void Reset()
        {
            Path = _pathCreator.GetRandomPath();
            _stateMachine.Enter<MoveToTargetState>();
        }
    }
}