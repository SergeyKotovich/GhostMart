using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class CharacterDestroyedEvent : EventBase
    {
        public Transform Transform { get; }

        public CharacterDestroyedEvent(Transform transform)
        {
            Transform = transform;
        }
    }
}