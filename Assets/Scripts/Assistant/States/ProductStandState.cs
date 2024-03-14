using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assistant
{
    public class ProductStandState : MonoBehaviour, IPayLoadedState<IStand>
    {
        private IWorker _assistant;
        private StateMachine _stateMachine;
        private IStand _stand;
        private int _transitionsToProductStandStateCount;

        private void Awake()
        {
            _assistant = GetComponent<IWorker>();
        }

        public void OnEnter(IStand payload)
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
                _stand.SetProductOnStand(product);
            }

            if (_assistant.Basket.IsEmpty())
            {
                if (_transitionsToProductStandStateCount>=0)
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