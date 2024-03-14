using DG.Tweening;
using UnityEngine;

namespace Island
{
    public class IslandScaler : MonoBehaviour
    {
        [SerializeField] private Vector3 _scaleShift;
        private Transform _currentIsland;
        public void IncreaseScale(Transform transform)
        {
            _currentIsland = transform;
            transform.DOScale(_currentIsland.localScale + _scaleShift, 0.5f);
        }

        public void DecreaseScale()
        {
            if (_currentIsland == null) return;
            transform.DOScale(_currentIsland.localScale + _scaleShift, 0.5f);
        }
    }
}