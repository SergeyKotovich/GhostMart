
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
        _productIcon.sprite = listItem.StopPoint.StandIcon;
        _productCount.text = listItem.CurrentCount + "/" + listItem.MaxCount;
    }
    
    public void UpdateProductBar(Sprite icon)
    {
        _productCount.text = "";
        _productIcon.sprite = icon;
    }
    
    public void UpdateProductBar(Sprite icon, int productsCount, int maxProductsCount)
    {
        _productCount.text = productsCount + "/" + maxProductsCount;
        _productIcon.sprite = icon;
    }
}