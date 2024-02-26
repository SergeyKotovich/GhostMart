using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;
    private float _balance;
    
    
    private void Start()
    {
        UpdateBalance();
    }
    
    public void AddMoney(float amount)
    {
        _balance += amount;
        UpdateBalance();
    }


    private void UpdateBalance()
    {
        _balanceText.text = _balance.ToString();
    }
    
    
}