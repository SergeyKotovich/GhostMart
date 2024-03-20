using System;
using UnityEngine;
using UnityEngine.AI;

namespace Assistant
{
    public class RecyclingProductsState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;
        private IDisposable _subscription;
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            _subscription = EventStreams.Global.Subscribe<ProductsAreRecycledEvent>(OnProductsAreRecycled);
        }
        
        private void OnProductsAreRecycled(ProductsAreRecycledEvent productsAreRecycledEvent)
        {
            _stateMachine.Enter<MovingToTargetState, TypeInteractablePoints>(TypeInteractablePoints.ProductFactory);
            _subscription.Dispose();
        }
    }
}