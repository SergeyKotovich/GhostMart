using UnityEngine;

public class WorkersImprover : MonoBehaviour
{
    [SerializeField] private Player _player;
    public void ImproveBasket(GameObject player)
    {
        var wallet = _player.Wallet;
        if (wallet.HasEnoughMoney(50))
        {
            var worker = player.GetComponent<IWorker>();
            var improver = worker.AbilitiesController;
            improver.ImproveBasket();
            wallet.SpendMoney(50);
        }
    }
}