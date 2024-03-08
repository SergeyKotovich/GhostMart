using System;
using Events;
using TMPro;
using UnityEngine;

public class ImprovementsView : MonoBehaviour
{
    [SerializeField] private WorkerImprovementLabel[] WorkerImprovementLabels;

    private void Start()
    {
        EventStreams.Global.Subscribe<WorkerUpgradedEvent>(UpdateView);
        EventStreams.Global.Subscribe<NewAssistantWasBoughtEvent>(SetImproveButtonInteractable);
    }

    private void UpdateView(WorkerUpgradedEvent workerUpgradedEvent)
    {
        for (int i = 0; i < WorkerImprovementLabels.Length; i++)
        {
            if (WorkerImprovementLabels[i].WorkerType == workerUpgradedEvent.WorkerType)
            {
                WorkerImprovementLabels[i].Label.text =
                    "Can pick up " + workerUpgradedEvent.MaxCountPlacesInBasket + " items";
            }
        }
    }

    private void SetImproveButtonInteractable(NewAssistantWasBoughtEvent newAssistantWasBoughtEvent)
    {
        for (int i = 0; i < WorkerImprovementLabels.Length; i++)
        {
            if (WorkerImprovementLabels[i].WorkerType == newAssistantWasBoughtEvent.Assistant.Type)
            {
                WorkerImprovementLabels[i].ImproveButton.interactable = true;
            }
        }
    }
}