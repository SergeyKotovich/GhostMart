using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class CharacterWasSpawnedEvent : EventBase
    {
        public Transform Transform { get; }

        public CharacterWasSpawnedEvent(Transform transform)
        {
            Transform = transform;
        }
    }
}