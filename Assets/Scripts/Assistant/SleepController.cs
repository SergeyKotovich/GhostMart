using System;
using DG.Tweening;
using UnityEngine;

namespace Assistant
{
    public class SleepController : MonoBehaviour, ISleepable
    {
        [field:SerializeField] public int MaxRepeatCount { get; private set; }
        
        [SerializeField] private GameObject[] _eyes;
        [SerializeField] private PlayerConfig _playerConfig;
        
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        
        public void ImproveRepetitionCount(int value)
        {
            MaxRepeatCount += value;
        }
        public void FellAsleep()
        {
            foreach (var eye in _eyes)
            {
                eye.transform.DOScale(_playerConfig.EyeSizeInDream, _playerConfig.Duration);
                // .OnComplete(() => _collider.isTrigger = true);
            }
        }

        public void WakeUp()
        {
            foreach (var eye in _eyes)
            {
                eye.transform.DOScale(_playerConfig.EyeSizeInWakefulness, _playerConfig.Duration);
            }
          //  _collider.isTrigger = false;
        }
    }
    
}