using UnityEngine;

public class WorkersImprover : MonoBehaviour
{
    [SerializeField] private Player.Player _player;
    public void ImproveLoadability(GameObject player)
    {
        var wallet = _player.Wallet;
        if (wallet.HasEnoughMoney(50))
        {
            var worker = player.GetComponent<IWorker>();
            var improver = worker.AbilitiesController;
            improver.ImproveLoadability();
            wallet.SpendMoney(50);
            Debug.Log("basket improved");
        }
    }

    public void ImproveSpeed(GameObject assistant)
    {
        var wallet = _player.Wallet;
        if (wallet.HasEnoughMoney(50))
        {
            var worker = assistant.GetComponent<IWorker>();
            var improver = worker.AbilitiesController;
            improver.ImproveSpeed();
            wallet.SpendMoney(50);
            Debug.Log("speed improved");
        }
    }

    public void ImproveEndurance(GameObject assistant)
    {
        var wallet = _player.Wallet;
        if (wallet.HasEnoughMoney(50))
        {
            var worker = assistant.GetComponent<IWorker>();
            var improver = worker.AbilitiesController;
            improver.ImproveEndurance();
            wallet.SpendMoney(50);
            Debug.Log("Endurance improved (no)");
        }
        
    }
}