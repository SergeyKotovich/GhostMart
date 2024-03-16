using DG.Tweening;
using TMPro;
using UnityEngine;

public class WorldMessageBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private float _messageDuration = 2f;
    
    
    public void ShowMessage(string message)
    {
        _messageText.text = message;
        _messageText.enabled = true;
        _messageText.DOFade(1, 0); 
        _messageText.DOFade(0, _messageDuration).OnComplete(() => _messageText.enabled = false); 
    }
}
