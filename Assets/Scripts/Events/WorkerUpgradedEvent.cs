using SimpleEventBus.Events;
using TMPro;

namespace Events
{
    public class WorkerUpgradedEvent : EventBase
    {
        public int MaxCountPlacesInBasket { get; }
        public WorkerTypes WorkerType { get; }

        public WorkerUpgradedEvent(int maxCountPlacesInBasket, WorkerTypes workerType)
        {
            MaxCountPlacesInBasket = maxCountPlacesInBasket;
            WorkerType = workerType;
        }
    }
}