using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BadCustomer
{
    public class DropProductState : MonoBehaviour, IPayLoadedState<IStand>
    {
        [SerializeField] private Animator _animator;
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
                var product = _stand.GetAvailableProduct();
                if (product != null)
                {
                    var xRandomPoint = Random.Range(-4.0f, -7.9f);
                    var zRandomPoint = Random.Range(-11.0f, -14.9f);

                    _animator.Play("Shity_attack");
                    await UniTask.Delay(1000);

                    product.transform.DOLocalMove(new Vector3(xRandomPoint, 0.1f, zRandomPoint), 0.3f)
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