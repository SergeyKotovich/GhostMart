using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BadCustomer
{
    public class DropProductState : MonoBehaviour, IPayLoadedState<IStand>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private BadCustomer _badCustomer;
        private StateMachine _stateMachine;
        private IStand _stand;
        private bool _trigger;

        public void OnEnter(IStand stand)
        {
            _stand = stand;
            DropProduct();
            _badCustomer._collider.isTrigger = true;
            _trigger = false;
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }


        private async UniTask DropProduct()
        {
            while (true)
            {
                var product = _stand.GetAvailableProduct();

                if (product != null && !_trigger)
                {
                    var xRandomPoint = Random.Range(-4.0f, -7.9f);
                    var zRandomPoint = Random.Range(-11.0f, -14.9f);

                    _animator.Play("Shity_attack");
                    await UniTask.Delay(1000);

                    product.transform.DOLocalMove(new Vector3(xRandomPoint, 0.1f, zRandomPoint), 0.3f);
                    await UniTask.Delay(4000);
                }

                if (_stand.IsEmpty() || _trigger)
                {
                    _stateMachine.Enter<WaitingState, IStand>(_stand);
                    break;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG))
            {
                _trigger = true;
            }
        }
    }
}