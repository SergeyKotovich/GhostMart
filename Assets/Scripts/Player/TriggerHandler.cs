using SparrowBonus;
using UnityEngine;

namespace Player
{
    public class TriggerHandler : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GlobalConstants.MONEY_KEEPER))
            {
                var moneySpawner = other.GetComponent<MoneySpawner>();
                _player.AddMoney(moneySpawner.GetMoney());
            }

            if (other.gameObject.CompareTag(GlobalConstants.BONUS_TAG))
            {
                var bonus = other.GetComponent<Bonus>();
                bonus.GetBonus(_player.Basket.GetSuitableProduct(TypeProduct.Corn));
            }
        }
    }
}