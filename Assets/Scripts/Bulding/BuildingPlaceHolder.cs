using System;
using UnityEngine;


public class BuildingPlaceHolder : MonoBehaviour
{
    [SerializeField] private GameObject _building;
    [SerializeField] private Wallet _wallet;
    [SerializeField] int _price;
    [SerializeField] private WorldMessageBox _messageBox;
    
    private Action<BuildingPlaceHolder> _onBuildingCompleted;

    public void Buy()
    {
        if (_wallet.HasEnoughMoney(_price))
        {
            _wallet.SpendMoney(_price);
            _building.SetActive(true);
            _onBuildingCompleted?.Invoke(this);
            gameObject.SetActive(false);
        }
        else
        {
            _messageBox.ShowMessage("Недостаточно денег для покупки");
        }
    }

    public void Show(Action<BuildingPlaceHolder> onBuildingCompleted)
    {
        _onBuildingCompleted = onBuildingCompleted;
        gameObject.SetActive(true);
    }
}