using System;
using Events;
using Interfaces;
using TMPro;
using UnityEngine;

public class AbilitiesController
{
    private IWorkerBasket _basket;
    private IWorker _worker;

    public AbilitiesController(IWorkerBasket basket, IWorker worker)
    {
        _basket = basket;
        _worker = worker;
    }
    public void ImproveBasket()
    {
         _basket.IncreaseMaxCountProduct();
         EventStreams.Global.Publish<WorkerUpgradedEvent>(new WorkerUpgradedEvent(_basket.MaxCountProduct, _worker.Type));
    }

    public void ImproveWorkerSpeed()
    {
        _worker.IncreaseSpeed();
    }

}