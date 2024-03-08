using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class BuildingSeller : MonoBehaviour
{
    [SerializeField] Wallet _wallet;
    [SerializeField] int _price;
    [SerializeField] UnityEvent _buyCompleted;
    [SerializeField] private BuildingManager _buildingManager;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private float _messageDuration = 2f;
    
    public void Buy()
    {
        if (_wallet.HasEnoughMoney(_price))
        {
            _wallet.SpendMoney(_price);
            _buyCompleted.Invoke();
            _buildingManager.BuildingCompleted();
            gameObject.SetActive(false);
        }
        else
        {
            ShowMessage("Недостаточно денег для покупки", _messageDuration);
        }
    }

    private void ShowMessage(string message, float delay)
    {
        _messageText.text = message;
        _messageText.enabled = true;
        _messageText.DOFade(1, 0); 
        _messageText.DOFade(0, delay).OnComplete(() => _messageText.enabled = false); 
    }
}