using UnityEngine;
public class WorkersImprover : MonoBehaviour
{
    [SerializeField] private Player.Player _player;
    [SerializeField] private int[] _price;  // должен ли вообще улучшатель в себе держать массив цен? или же нужно его вынести в отдельный класс Price [Serializable] ?

    private const int _indexShift = 1;

    public void ImproveLoadability(GameObject player)
    {
        var wallet = _player.Wallet;
        var level = _player.AbilitiesController.LoadabilityLevel-_indexShift;
        //var text = gameObject.GetComponent<TextMeshPro>(); надо подумать как сюда передать кнопку у которой менять текст
        
        if (wallet.HasEnoughMoney(_price[level]))
        {
            var worker = player.GetComponent<IWorker>();
            var improver = worker.AbilitiesController;
            improver.ImproveLoadability();
            wallet.SpendMoney(_price[level]);
            //text.text = _price[level].ToString();
            Debug.Log("basket improved" + " price " + _price[level]);
        }
    }

    public void ImproveSpeed(GameObject assistant)
    {
        var wallet = _player.Wallet;
        var level = _player.AbilitiesController.SpeedLevel-_indexShift;
        
        if (wallet.HasEnoughMoney(_price[level]))
        {
            var worker = assistant.GetComponent<IWorker>();
            var improver = worker.AbilitiesController;
            improver.ImproveSpeed();
            wallet.SpendMoney(_price[level]);
            Debug.Log("speed improved");
        }
    }

    public void ImproveEndurance(GameObject assistant)
    {
        var wallet = _player.Wallet;
        var level = _player.AbilitiesController.EnduranceLevel-_indexShift;
        
        if (wallet.HasEnoughMoney(_price[level]))
        {
            var worker = assistant.GetComponent<IWorker>();
            var improver = worker.AbilitiesController;
            improver.ImproveEndurance();
            wallet.SpendMoney(_price[level]);
            Debug.Log("Endurance improved (no)");
        }
        
    }
}