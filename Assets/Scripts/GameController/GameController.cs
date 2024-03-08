using System;
using Customer;
using Events;
using UnityEngine;

namespace GameController
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private BuildingManager _buildingManager;
        [SerializeField] private CustomersController _customersController;

        private void Awake()
        {
            EventStreams.Global.Subscribe<MartOpenedEvent>(OnMartOpened);
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
    }
}