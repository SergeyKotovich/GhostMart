using System;
using Events;
using Improver;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class AbilitiesController
{
    [SerializeField] private AbilitiesConfig _abilitiesConfig;
    public int LoadabilityLevel { get; private set; }
    public int EnduranceLevel { get; private set; }
    public int SpeedLevel { get; private set; }
    
    private IWorkerBasket _basket;
    private IWorker _worker;
    private NavMeshAgent _navMeshAgent;

    public void Initialize(IWorkerBasket basket, IWorker worker, NavMeshAgent navMeshAgent)
    {
        _basket = basket;
        _worker = worker;
        _navMeshAgent = navMeshAgent;
        LoadabilityLevel = _abilitiesConfig.DefaultAbilityLevel;
        EnduranceLevel = _abilitiesConfig.DefaultAbilityLevel;
        SpeedLevel = _abilitiesConfig.DefaultAbilityLevel;
    }
    public void Initialize(IWorkerBasket basket, IWorker worker)
    {
        _basket = basket;
        _worker = worker;
        LoadabilityLevel = 1;
        EnduranceLevel = 1;
        SpeedLevel = 1;
    }
    public void ImproveLoadability()
    {
        LoadabilityLevel++;
        _basket.IncreaseMaxCountProduct();
        EventStreams.Global.Publish<WorkerUpgradedEvent>
            (new WorkerUpgradedEvent(LoadabilityLevel, _worker.Type, AbilityTypes.Loadability));
    }

    public void ImproveSpeed()
    {
        SpeedLevel++;
        var currentSpeed = _navMeshAgent.speed;
        _navMeshAgent.speed = currentSpeed + 1;
        EventStreams.Global.Publish<WorkerUpgradedEvent>
            (new WorkerUpgradedEvent(SpeedLevel, _worker.Type, AbilityTypes.Speed));
    }

    public void ImproveEndurance()
    {
        EnduranceLevel++;
        var assistant = (ISleepable)_worker;
        assistant.ImproveRepetitionCount(_abilitiesConfig.ImprovingStep);
        
        EventStreams.Global.Publish<WorkerUpgradedEvent>
            (new WorkerUpgradedEvent(EnduranceLevel, _worker.Type, AbilityTypes.Endurance));
    }
    

}