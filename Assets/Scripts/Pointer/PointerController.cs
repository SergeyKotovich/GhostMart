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
        private void Update()
        {
            FollowPlayer();
            PointAtTarget();
        }

        private void FollowPlayer()
        {
            var newPosition = new Vector3(_playerTransform.position.x, 4.5f, _playerTransform.position.z);
            transform.position = newPosition;
        }

        private void PointAtTarget()
        {
            transform.LookAt(_targetTansform);
        }

        public async UniTask SetNewTarget(Transform targetTransform)
        {
            _targetTansform = targetTransform;
            await UniTask.Delay(2000);
            ShowPointer();
        }

        public async UniTask ShowPointer()
        {
            gameObject.SetActive(true);
            var defaultScale = transform.localScale;
            transform.DOPunchScale(new Vector3(50f, 50f, 50f), 0.2f);
            transform.localScale = defaultScale;
            
            await UniTask.Delay(10000);
            HidePointer();
        }
        
        public void HidePointer()
        {
            transform.DOPunchScale(new Vector3(50f, 50f, 50f), 0.2f).
                OnComplete(() => gameObject.SetActive(false));
            var defaultScale = transform.localScale;
            transform.localScale = defaultScale;
        }
    }
}