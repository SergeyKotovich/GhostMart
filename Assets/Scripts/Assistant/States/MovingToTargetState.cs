using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Assistant
{
    
    public class MovingToTargetState : MonoBehaviour, IPayLoadedState<InteractableTypes>
    {
        [SerializeField] private List<Transform> _pointPath;
        [SerializeField] private Transform _pointForRecycling;
        [SerializeField] private Transform _pointForSleeping;
        
        private int _currentIndex;
        
        private StateMachine _stateMachine;
        
        private IFactory _productFactory;
        private IStorageable _stand;
        private IMovable _assistant;
        private InteractableTypes _type;

        private void Awake()
        {
            _assistant = GetComponent<IMovable>();
        }
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void OnEnter(InteractableTypes payload)
        {
            _type = payload;
            MoveToPoint();
        }

        private void Update()
        {
            if (_assistant.MovementController.IsAtTargetPoint())
            {
                EnterNextState();
            }
        }

        private void MoveToPoint()
        {
            switch (_type)
            {
                case InteractableTypes.Stand:
                    SetDestination(_pointPath[_currentIndex].position);
                    SetNextPoint();
                    return;
                case InteractableTypes.Recycling:
                    SetDestination(_pointForRecycling.position);
                    return;
                case InteractableTypes.SleepPoint:
                    SetDestination(_pointForSleeping.position);
                    return;
                case InteractableTypes.ProductFactory:
                    SetDestination(_pointPath[_currentIndex].position);
                    SetNextPoint();
                    break;
            }
        }

        private void SetDestination(Vector3 destination)
        {
            _assistant.MovementController.SetDestination(destination);
        }

        private void SetNextPoint()
        {
            _currentIndex = (_currentIndex + 1) % _pointPath.Count;
        }
        
        private void EnterNextState()
        {
            switch (_type)
            {
                case InteractableTypes.ProductFactory:
                    _stateMachine.Enter<CollectingProductsState, IFactory>(_productFactory);
                    return;
                case InteractableTypes.Stand:
                    _stateMachine.Enter<ProductStandState, IStorageable>(_stand);
                    return;
                case InteractableTypes.Recycling:
                    _stateMachine.Enter<RecyclingProductsState>();
                    return;
                case InteractableTypes.SleepPoint:
                    _stateMachine.Enter<SleepingState>();
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PRODUCT_FACTORY_TAG))
            {
                _productFactory = other.GetComponent<IFactory>();
            }

            if (other.CompareTag(GlobalConstants.STAND_TAG))
            {
                _stand = other.GetComponent<IStorageable>();
            }
        }
    }
}
