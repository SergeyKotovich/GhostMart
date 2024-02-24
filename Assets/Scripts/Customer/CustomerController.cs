using System;
using UnityEngine;

namespace Customer
{
    public class CustomerController : MonoBehaviour
    {
        private void Start()
        {
            var stateMachine = new StateMachine
            (
                GetComponent<MovingToTargetState>(),
                GetComponent<PayingProductsState>(),
                GetComponent<GettingProductsState>()
            );
            
            stateMachine.Initialize();
            stateMachine.Enter<MovingToTargetState>();
        }
    }
}