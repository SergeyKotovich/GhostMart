using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Assistant
{
    public class SleepingState : MonoBehaviour, IState
    {
        [SerializeField] private GameObject[] _eyes;
        private StateMachine _stateMachine;
        private ISleepable _assistant;

        private void Awake()
        {
            _assistant = GetComponent<ISleepable>();
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void OnEnter()
        {
            
            foreach (var eye in _eyes)
            {
                eye.transform.DOScale(new Vector3(1, 1, -0.2f), 4)
                    .OnComplete(() => _assistant.Collider.isTrigger = true);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG))
            {
                foreach (var eye in _eyes)
                {
                    eye.transform.DOScale(new Vector3(1, 1, 1), 1);
                }
                
                _assistant.SetSleepingState(false);
                _assistant.Collider.isTrigger = false;
                _stateMachine.Enter<MovingToTargetState>();
            }
        }
    }
}