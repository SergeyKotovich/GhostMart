using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BadCustomer
{
    public class DropProductState : MonoBehaviour, IPayLoadedState<IStand>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _moveRadius;
        private StateMachine _stateMachine;
        private IStand _stand;
        private bool _didPlayerComeUp;
        private bool _isActive;

        public void OnEnter(IStand stand)
        {
            _stand = stand;
            _didPlayerComeUp = false;
            _isActive = true;
            DropProduct();
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        private async UniTask DropProduct()
        {
            while (!_didPlayerComeUp)
            {
                if (_stand.IsEmpty())
                {
                    break;
                }
                var product = _stand.GetAvailableProduct();
                var dropPoint = _stand.GetDropPoint();
                
                Vector2 randomOffset = Random.insideUnitCircle * _moveRadius;
                Vector3 randomPosition = new Vector3(randomOffset.x, 0.1f, randomOffset.y) + dropPoint.transform.position;
                
                if (product != null)
                {
                    _animator.Play("Shity_attack");
                    await UniTask.Delay(1000);

                    product.transform.DOLocalMove(randomPosition, 0.3f)
                        .OnComplete(() => product.OnProductWasDropped());
                    await UniTask.Delay(4000);
                }
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG) && _isActive)
            {
                _didPlayerComeUp = true;
                _isActive = false;
                _stateMachine.Enter<MoveToTargetState>();
            }
        }
    }
}