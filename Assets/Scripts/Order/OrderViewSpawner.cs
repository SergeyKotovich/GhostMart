using System;
using Events;
using UnityEngine;

namespace Order
{
    public class OrderViewSpawner : MonoBehaviour
    {

        [SerializeField] private OrderView orderPrefab;
        [SerializeField] private Canvas _canvas;
        private IDisposable _subscription;

        private void Awake()
        {
            _subscription = EventStreams.Global.Subscribe<CharacterWasSpawnedEvent>(Spawn);
        }

        private void Spawn(CharacterWasSpawnedEvent characterWasSpawnedEvent)
        {
            var characterTransform = characterWasSpawnedEvent.Transform;
            var productBarView = Instantiate(orderPrefab, _canvas.transform);

            var uIElementPositionController = productBarView.GetComponent<UIElementPositionController>();
            uIElementPositionController.Initialize(characterTransform);
            productBarView.Initialize(characterTransform);
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}