using System;
using Customer;
using Events;
using SimpleEventBus.Disposables;
using UnityEngine;

namespace GameController
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private BuildingManager _buildingManager;
        [SerializeField] private CustomersController _customersController;

        private CompositeDisposable _subscribers = new();
        private void Awake()
        {
            _subscribers.Add(EventStreams.Global.Subscribe<MartOpenedEvent>(OnMartOpened));
            StartGame();
        }

        private void StartGame()
        {
            _buildingManager.StartBuilding();
        }

        private void OnMartOpened(MartOpenedEvent martOpenedEvent)
        {
            _customersController.Initialize();
        }

        private void OnDestroy()
        {
            _subscribers.Dispose();
        }
    }
}