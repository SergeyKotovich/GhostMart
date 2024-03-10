using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Assistant
{
    public class MovingToTargetState : MonoBehaviour, IState
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private List<Transform> _pointPath;
        [SerializeField] private NextPointController _nextPointController;

        private readonly int IsMoving = Animator.StringToHash("IsMoving");
        private bool _isMoving;
        private StateMachine _stateMachine;
        private bool _isProductFactory;
        private bool _isStand;
        private IFactory _productFactory;
        private IStand _stand;
        private int _currentIndex;
        private FactoryStand _factoryStand;
        private Stand _currentStand;


        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Update()
        {
            if (!(_navMeshAgent.remainingDistance < 0.1f)) return;
            EnterNextState();
            _isStand = false;
            _isProductFactory = false;
        }

        private void MoveToPoint(Vector3 currentPoint)
        {
            _isMoving = true;
            _animator.SetBool(IsMoving, _isMoving);
            _navMeshAgent.SetDestination(currentPoint);
        }

        private void GetNextPoint()
        {
            _factoryStand = _nextPointController.GetRandomNextPoint();
            var currentPoint = _factoryStand.ProductFactory.gameObject.transform.position;
            if (_currentStand != null)
            {
                currentPoint = _currentStand.PointForCustomers.position;
                _currentStand = null;
                MoveToPoint(currentPoint);
                return;
            }
            MoveToPoint(currentPoint);
            _currentStand = _factoryStand.Stand;
        }


        public void OnEnter()
        {
            GetNextPoint();
        }

        private void EnterNextState()
        {
            if (_isProductFactory)
            {
                _animator.SetBool(IsMoving, false);
                _stateMachine.Enter<CollectingProductsState, IFactory>(_productFactory);
            }

            if (_isStand)
            {
                _animator.SetBool(IsMoving, false);
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