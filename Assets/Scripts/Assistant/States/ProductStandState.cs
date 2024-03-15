using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assistant
{
    public class ProductStandState : MonoBehaviour, IPayLoadedState<IStorageable>
    {
        private IWorker _assistant;
        private StateMachine _stateMachine;
        private IStorageable _stand;
        private int _repeatCounter;

        private void Awake()
        {
            _assistant = GetComponent<IWorker>();
        }

        public void OnEnter(IStorageable payload)
        {
            _stand = payload;
            if (!_assistant.Basket.IsEmpty())
            {
                if (_stand.IsFull())
                {
                    var assistant = (IRecyclable)_assistant;
                    assistant.SetRecyclingState(true);;
                    _stateMachine.Enter<MovingToTargetState>();
                }
                else
                {
                    SetProductOnStand();
                }
            }
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void SetProductOnStand()
        {
            while (!_assistant.Basket.IsEmpty())
            {
                if (_stand.IsFull())
                {
                    var assistant = (IRecyclable)_assistant;
                    assistant.SetRecyclingState(true);
                    _stateMachine.Enter<MovingToTargetState>();
                    return;
                }

                var product = _assistant.Basket.GetProduct();
                _stand.AddProduct(product);
            }

            if (_assistant.Basket.IsEmpty())
            {
                var assistant = (ISleepable)_assistant; 
                if (_repeatCounter>= assistant.MaxRepeatCount)
                {
                    assistant.SetSleepingState(true);
                    _repeatCounter = 0;
                    _stateMachine.Enter<MovingToTargetState>();
                    return;
                }
                _repeatCounter++;
                _stateMachine.Enter<MovingToTargetState>();
            }
        }
       
    }
}