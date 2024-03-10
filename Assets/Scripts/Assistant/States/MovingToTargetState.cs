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
        [SerializeField] private NextPointController _nextPointController;

        private readonly int IsMoving = Animator.StringToHash("IsMoving");
        private bool _isMoving;
        private StateMachine _stateMachine;
        private bool _isProductFactory;
        private bool _isStand;
        private IFactory _productFactory;
        private IStand _stand;
        private Vector3 _currentPoint;
        private FactoryStand _factoryStand;
        private IInteractable _currentStand;


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
        
        public void OnEnter()
        {
        //  var factoryStand = _nextPointController.GetRandomNextPoint();
        //  var currentPoint = Vector3.zero;
        //  if (_currentStand==null)
        //  {
        //      _currentStand = factoryStand.Stand;
        //      currentPoint = factoryStand.ProductFactory.gameObject.transform.position;
        //      MoveToPoint(currentPoint);
        //      return;
        //  }
        //  currentPoint = _currentStand.PointForCustomers.position;
        //  _currentStand = null;
        //  MoveToPoint(currentPoint);
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