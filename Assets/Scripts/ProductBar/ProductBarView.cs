
using Customer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductBarView : MonoBehaviour
{
    [SerializeField] private Image _productIcon;
    [SerializeField] private TextMeshProUGUI _productCount;
    public void UpdateProductBar(ListItem listItem)
    {
        if (listItem.StopPoint.Type == TypeProduct.CashRegister)
        {
            _productCount.text = "";
            _productIcon.sprite = listItem.StopPoint.StandIcon;
            return;
        }
        _productIcon.sprite = listItem.StopPoint.StandIcon;
        _productCount.text = listItem.CurrentCount + "/" + listItem.MaxCount;
    }
}