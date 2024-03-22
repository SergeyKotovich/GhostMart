using Customer;
using Order;
using SimpleEventBus.Events;
using UnityEngine;

namespace Events
{
    public class OrderUpdatedEvent : EventBase
    {
        public IOrder CurrentOrder { get; }
        public Transform Transform { get; }

        public OrderUpdatedEvent(IOrder currentOrder, Transform transform)
        {
            CurrentOrder = currentOrder;
            Transform = transform;
        }
    }
}