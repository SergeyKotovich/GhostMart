using Interfaces;
using UnityEngine;

namespace BadCustomer
{
    public class MoveToTargetState : MonoBehaviour, IState
    {
        private IBadCustomer _badCustomer;
        private StateMachine _stateMachine;
        private IInteractable _currentPoint;
        private int _currentPathIndex;
        private bool _isActive;

        private void Awake()
        {
            _badCustomer = GetComponent<IBadCustomer>();
        }

        private void Update()
        {
            if (!_isActive || !_badCustomer.IsAtTargetPoint())return;
            
            OnCameToTarget();
        }
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void OnEnter()
        {
            _currentPoint = _badCustomer.Path[_currentPathIndex];
            MoveToTarget(_currentPoint.PointForCustomers);
        }

        private void MoveToTarget(Transform destinationTransform)
        {
            _badCustomer.SetDestination(destinationTransform.position);
            _isActive = true;
        }

        private void EnterDropProductState()
        {
            var currentStand = (IStand)_currentPoint;
            _stateMachine.Enter<DropProductState, IStand>(currentStand);
            _isActive = false;
        }

        private void OnCameToTarget()
        {
            if (_currentPoint.TypeInteractablePoint == TypeInteractablePoints.Exit)
            {
                _currentPathIndex = 0;
                _isActive = false;
                EventStreams.Global.Publish(new CameToExitEvent());
                return;
            }

            if (_currentPoint.TypeInteractablePoint == TypeInteractablePoints.Stand)
            {
                _currentPathIndex++;
                _isActive = false;
                EnterDropProductState();
            }
        }
    }
}