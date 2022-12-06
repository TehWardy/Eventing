using EventLibrary.Brokers.Interfaces;
using EventLibrary.Services.Foundation.Interfaces;

namespace EventLibrary.Services.Foundation
{
    public class EventService<T> : IEventService<T>
    {
        readonly IEventBroker<T> eventStorageBroker;

        public EventService(IEventBroker<T> eventStorageBroker) =>
            this.eventStorageBroker = eventStorageBroker;

        public void ListenToEvent(string name, Func<T, ValueTask> handler) =>
            eventStorageBroker.ListenToEvent(name, handler);

        public async ValueTask RaiseEventAsync(string name, T data)
        {
            IEnumerable<Func<T, ValueTask>> handlers = 
                eventStorageBroker.GetHandlers(name);

            foreach (var handler in handlers)
                await handler.Invoke(data);
        }
    }
}