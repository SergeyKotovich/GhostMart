using System.Collections;
using System.Collections.Generic;
using Customer;
using Events;
using Pointer;
using SimpleEventBus.Events;
using TMPro;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> buildings;
    [SerializeField] private GameObject hintCanvas;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private float delayBeforeNextHint = 2f;
    [SerializeField] private PointerController _pointerController;
    public void StartBuilding()
    {
        //buildings[0].SetActive(true);
        //buildings.RemoveAt(0);
    }
    public void BuildingCompleted()
    {
        if (buildings.Count == 0)
        {
            hintCanvas.SetActive(false);
            return;
        }
        
        if (buildings[0].CompareTag(GlobalConstants.BANANA_TAG))
        {
            EventStreams.Global.Publish(new MartOpenedEvent());
        }
        
        buildings.RemoveAt(0);
        StartCoroutine(ShowDelayedHint(buildings[0]));
        _pointerController.SetNewTarget(buildings[0].transform);
    }
    private IEnumerator ShowDelayedHint(GameObject building)
    {
        hintText.text = "Супер ты молодец!";
        yield return new WaitForSeconds(delayBeforeNextHint);

        building.SetActive(true);
        hintText.text = $"Необходимо построить {building.name}";
    }
}