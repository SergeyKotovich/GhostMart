using DG.Tweening;
using UnityEngine;

namespace SparrowBonus
{
    public class MoneySpawner : MonoBehaviour
    {
        [SerializeField] private MoneySpawnerConfig _moneySpawnerConfig;
        [SerializeField] private GameObject _moneyPrefab;
        [SerializeField] private int _maxMoneyReward;
        public void Spawn()
        {
            var prefabRotation = _moneyPrefab.transform.rotation;
            var shift = 0;
            
            for (int i = 0; i < _maxMoneyReward; i++)
            {
                var bonus = Instantiate(_moneyPrefab, transform.position, prefabRotation);

                var x = bonus.transform.position.x;
                var z = bonus.transform.position.z;
                var newPosition = new Vector3(x + shift, 0, z - shift);
                
                bonus.transform.DOLocalMove(newPosition, _moneySpawnerConfig.AnimationDuration);
                bonus.transform.DOScale(_moneySpawnerConfig.Scale, _moneySpawnerConfig.AnimationDuration);

                shift++;
            }
        }
    }
}