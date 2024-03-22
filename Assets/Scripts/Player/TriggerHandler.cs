using SparrowBonus;
using UnityEngine;

namespace Player
{
    public class TriggerHandler : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GlobalConstants.CASH_REGISTER))
            {
                var cashRegister = other.GetComponent<ICashRegister>();
                if (cashRegister == null) return;
                
                _player.AddMoney(cashRegister.GetMoney());
            }

            if (other.gameObject.CompareTag(GlobalConstants.BONUS_TAG))
            {
                var bonus = other.GetComponent<Bonus>();
                bonus.GetBonus(_player.Basket.GetSuitableProduct(TypeProduct.Corn));
            }
        }
    }
}