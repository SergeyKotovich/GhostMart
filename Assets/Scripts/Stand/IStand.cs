using System.Collections.Generic;
using UnityEngine;

public interface IStand
{
    public List<StandCell> StandCells { get; }
    public TypeProduct TypeProduct { get; }

    public int GetProductsCount();
    public Product GetAvailableProduct();
    public void SetProductOnStand(Product product);
    public bool IsFull();
}