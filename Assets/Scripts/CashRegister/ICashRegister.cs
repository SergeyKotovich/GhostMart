using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interfaces;
using UnityEngine;

public interface ICashRegister
{
    public UniTask SellProducts(List<Product> products);
    public Vector3 GetFreePosition(ICustomer customer);
    public void OnCustomerLeft(ICustomer customer);

}