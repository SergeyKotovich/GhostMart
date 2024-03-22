using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Pointer
{
    public class PointerController : MonoBehaviour
    {
        [SerializeField] private Transform _targetTansform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PointerConfig _pointerConfig;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        private void Update()
        {
            FollowPlayer();
            PointAtTarget();
        }
        
        public async UniTask SetNewTarget(Transform targetTransform)
        {
            _targetTansform = targetTransform;
            await UniTask.Delay(_pointerConfig.DelayBeforeNextTarget);
            ShowPointer();
        }
        
        public void HidePointer()
        {
            transform.DOScale(Vector3.zero, _pointerConfig.AnimationDelay).
                OnComplete(() => gameObject.SetActive(false));
        }

        private void FollowPlayer()
        {
            var newPosition = new Vector3(_playerTransform.position.x, _pointerConfig.OffsetY, _playerTransform.position.z);
            transform.position = newPosition;
        }

        private void PointAtTarget()
        {
            transform.LookAt(_targetTansform);
        }

        private void ShowPointer()
        {
            gameObject.SetActive(true);
            transform.DOScale(_initialScale, _pointerConfig.AnimationDelay);
        }
    }
}