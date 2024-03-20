using UnityEngine;

public class MoneyBonus : MonoBehaviour
{
    [SerializeField] private int _moneyValue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            var player = other.GetComponent<Player.Player>();
            player.AddMoney(_moneyValue);
            Destroy(gameObject);
        }
    }
    
}
