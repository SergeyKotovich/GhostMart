using Improver;
using SimpleEventBus.Events;
using TMPro;

namespace Events
{
    public class WorkerUpgradedEvent : EventBase
    {
        public int CurrentLevel { get; }
        public WorkerTypes WorkerType { get; }
        public AbilityTypes AbilityType { get; }

        public WorkerUpgradedEvent(int currentLevel, WorkerTypes workerType, AbilityTypes abilityType)
        {
            CurrentLevel = currentLevel;
            WorkerType = workerType;
            AbilityType = abilityType;
        }
    }
}