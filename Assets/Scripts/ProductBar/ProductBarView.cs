
using System;
using System.Collections.Generic;
using Customer;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductBarView : MonoBehaviour
{
    [SerializeField] private Image _productIcon;
    [SerializeField] private TextMeshProUGUI _productCount;
    public void UpdateProductBar(ListItem listItem)
    {
        if (listItem.StopPoint.Type == StandsTypes.CashRegister)
        {
            _productCount.text = "";
            _productIcon.sprite = listItem.StopPoint.StandIcon;
            return;
        }
        _productIcon.sprite = listItem.StopPoint.StandIcon;
        _productCount.text = listItem.CurrentCount + "/" + listItem.MaxCount;
    }
}