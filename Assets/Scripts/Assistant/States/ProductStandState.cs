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
        private int _transitionsToProductStandStateCount;

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
                    EventStreams.Global.Publish(new StandIsFoolEvent());
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
                    EventStreams.Global.Publish(new StandIsFoolEvent());
                    _stateMachine.Enter<MovingToTargetState>();
                    break;
                }

                var product = _assistant.Basket.GetProduct();
                _stand.AddProduct(product);
            }

            if (_assistant.Basket.IsEmpty())
            {
                if (_transitionsToProductStandStateCount>= 20)
                {
                    var assistant = (ISleepable)_assistant;
                    assistant.SetSleepingState(true);
                    _transitionsToProductStandStateCount = 0;
                    _stateMachine.Enter<MovingToTargetState>();
                    return;
                }
                _transitionsToProductStandStateCount++;
                _stateMachine.Enter<MovingToTargetState>();
            }
        }
       
    }
}