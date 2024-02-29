using SimpleEventBus;
using SimpleEventBus.Interfaces;

public static class EventStreams
{
    public static IEventBus Global { get; } = new EventBus();
}