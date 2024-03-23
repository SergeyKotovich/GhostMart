using System;
using Events;
using Improver;
using TMPro;
using UnityEngine;

public class ImprovementsView : MonoBehaviour
{
    [SerializeField] private AbilityLabel[] _abilityLabels;

    private void Start()
    {
        EventStreams.Global.Subscribe<WorkerUpgradedEvent>(UpdateView);
    }

    private void UpdateView(WorkerUpgradedEvent workerUpgradedEvent)
    {
        for (int i = 0; i < _abilityLabels.Length; i++)
        {
            if (_abilityLabels[i].WorkerType == workerUpgradedEvent.WorkerType)
            {
                var assistantLabel = _abilityLabels[i];
                for (int j = 0; j < assistantLabel.AbilityTypes.Length; j++)
                {
                    if (assistantLabel.AbilityTypes[j] == workerUpgradedEvent.AbilityType)
                    {
                        assistantLabel.TextLabel[j].text = "Lv. " + workerUpgradedEvent.CurrentLevel;
                    }
                }
            }
        }
    }
}