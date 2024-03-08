using SimpleEventBus.Events;

namespace Events
{
    public class NewAssistantWasBoughtEvent : EventBase
    {
        public Assistant Assistant { get; }

        public NewAssistantWasBoughtEvent(Assistant assistant)
        {
            Assistant = assistant;
        }
    }
}