using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assistant
{
    public class RecyclingProductsState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;
        private IDisposable _subscription;

        private void Awake()
        {
            _subscription = EventStreams.Global.Subscribe<ProductsAreRecycledEvent>(OnProductsAreRecycled);
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
           
        }
        
        private void OnProductsAreRecycled(ProductsAreRecycledEvent productsAreRecycledEvent)
        {
            _stateMachine.Enter<MovingToTargetState, InteractableTypes>(InteractableTypes.ProductFactory);
        }

        private void OnDestroy()
        {
            _subscription.Dispose();
        }
    }
}