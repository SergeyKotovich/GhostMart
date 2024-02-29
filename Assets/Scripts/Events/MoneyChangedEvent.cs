using SimpleEventBus.Events;

namespace Events
{
    public class MoneyChangedEvent : EventBase
    {
        public int Money { get; }

        public MoneyChangedEvent(int money)
        {
            Money = money;
        }
    }
}