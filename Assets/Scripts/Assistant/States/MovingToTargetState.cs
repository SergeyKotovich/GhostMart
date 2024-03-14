using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interfaces;
using SimpleEventBus.Disposables;
using UnityEngine;
using UnityEngine.AI;

namespace Assistant
{
    public class MovingToTargetState : MonoBehaviour, IState
    {
        [SerializeField] private List<Transform> _pointPath;
        [SerializeField] private Transform _pointForRecycling;
        [SerializeField] private Transform _pointForSleeping;

        private bool _isProductFactory;
        private bool _isStand;
        
        private int _currentIndex;
        
        private StateMachine _stateMachine;
        
        private IFactory _productFactory;
        private IStand _stand;
        private IMovable _assistant;

        private void Awake()
        {
            _assistant = GetComponent<IMovable>();
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Update()
        {
            if (_assistant.MovementController.IsAtTargetPoint())
            {
                EnterNextState();
                _isStand = false;
                _isProductFactory = false;
            }
        }

        private void MoveToPoint()
        {
            var recyclableAssistant = (IRecyclable)_assistant;
            if (recyclableAssistant.ISRecycling)
            {
                _assistant.MovementController.SetDestination(_pointForRecycling.position);
                _stateMachine.Enter<RecyclingProductsState>();
                return;
            }

            var sleepableAssistant = (ISleepable)_assistant;
            if (sleepableAssistant.IsSleeping)
            {
                _assistant.MovementController.SetDestination(_pointForSleeping.position);
                _stateMachine.Enter<SleepingState>();
                return;
            }
            _assistant.MovementController.SetDestination(_pointPath[_currentIndex].position);
            SetNextPoint();
        }

        private void SetNextPoint()
        {
            _currentIndex = (_currentIndex + 1) % _pointPath.Count;
        }

        public void OnEnter()
        {
            MoveToPoint();
        }
       
        private void EnterNextState()
        {
            if (_isProductFactory)
            {
                _stateMachine.Enter<CollectingProductsState, IFactory>(_productFactory);
            }

            if (_isStand)
            {
                _stateMachine.Enter<ProductStandState, IStand>(_stand);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
            {
                _isProductFactory = true;
                _productFactory = other.GetComponent<IFactory>();
            }

            if (other.CompareTag(GlobalConstants.STAND_TAG))
            {
                _isStand = true;
                _stand = other.GetComponent<IStand>();
            }
        }
    }
}
