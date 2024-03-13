using SimpleEventBus.Events;

namespace Events
{
    public class NewAssistantWasBoughtEvent : EventBase
    {
        public Assistant.Assistant Assistant { get; }

        public NewAssistantWasBoughtEvent(Assistant.Assistant assistant)
        {
            Assistant = assistant;
        }
    }
}