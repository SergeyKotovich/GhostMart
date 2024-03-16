using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Events;
using Pointer;
using TMPro;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    [SerializeField] private List<BuildingPlaceHolder> _buildingPlaceHolders;
    [SerializeField] private GameObject _hintCanvas;
    [SerializeField] private TextMeshProUGUI _hintText;
    [SerializeField] private float _delayBeforeNextHint = 2f;
    [SerializeField] private PointerController _pointerController;
    public void StartBuilding()
    { 
        ShowNextPlaceHolder();
    }

    private async void OnBuildingCompleted(BuildingPlaceHolder buildingPlaceHolder)
    {
        if (_buildingPlaceHolders[0].CompareTag(GlobalConstants.BANANA_TAG))
        {
            EventStreams.Global.Publish(new MartOpenedEvent());
        }
        _buildingPlaceHolders.Remove(buildingPlaceHolder);
        
        ShowHintGoodJob();
        await UniTask.Delay((int)(_delayBeforeNextHint * 1000));
        
        if (_buildingPlaceHolders.Count > 0) 
        {
            ShowHintWithNextBuilding(_buildingPlaceHolders[0]);
            await _pointerController.SetNewTarget(_buildingPlaceHolders[0].transform);
        }
        ShowNextPlaceHolder();
    }

    private void ShowNextPlaceHolder()
    {
        if (_buildingPlaceHolders.Count == 0)
        {
            _hintCanvas.SetActive(false);
            return;
        }
        _buildingPlaceHolders[0]?.Show(OnBuildingCompleted);
    }
    

    private void ShowHintGoodJob()
    {
        _hintText.text = "Супер ты молодец!";
    }

    private void ShowHintWithNextBuilding(Object building)
    {
        _hintText.text = $"Необходимо построить {building.name}";
    }
}
