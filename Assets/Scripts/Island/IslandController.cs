using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Island
{
    public class IslandController : MonoBehaviour
    {
        [SerializeField] private IslandControllerConfig _islandControllerConfig;
        private Vector3 _defaultScale;

        private void Awake()
        {
            _defaultScale = transform.localScale;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG))
            {
                var percentIncrease = _islandControllerConfig.PercentageIncrease;
                var scaleShift = new Vector3(_defaultScale.x * percentIncrease, _defaultScale.y * percentIncrease, _defaultScale.z);
                
                transform.DOScale(_defaultScale + scaleShift, _islandControllerConfig.Duration);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GlobalConstants.PLAYER_TAG))
            {
                transform.DOScale(_defaultScale, _islandControllerConfig.Duration);
            }
        }
    }
}