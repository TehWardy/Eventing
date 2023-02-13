using EventLibrary.Brokers.Interfaces;
using EventLibrary.Objects;
using EventLibrary.Objects.Interfaces;
using EventLibrary.Services.Foundation;
using EventLibrary.Services.Processing;
using EventLibrary.Services.Processing.Interfaces;

namespace EventLibrary
{
    public class EventHub : IEventHub
    {
        readonly List<object> services = new();
        readonly Func<IEventAuthInfo> getAuthInfo;

        public EventHub(Func<IEventAuthInfo> getAuthInfo)
        {
            services = new List<object>();
            this.getAuthInfo = getAuthInfo;
        }

        public void ListenToEvent<T>(string name, Func<T, ValueTask> handler) =>
            GetEventService<T>().ListenToEvent(name, handler);

        public async ValueTask RaiseEventAsync<T>(string name, T data) =>
            await GetEventService<T>().RaiseEventAsync(name, data);

        IEventProcessingService<T> GetEventService<T>() =>
            services.FirstOrDefault(s => s.GetType().GenericTypeArguments[0] == typeof(T)) as IEventProcessingService<T>
                    ??
                CreateEventService<T>();

        IEventProcessingService<T> CreateEventService<T>()
        {
            var service = new EventProcessingService<T>(
                new EventService<EventMessage<T>>(new EventBroker<EventMessage<T>>()),
                getAuthInfo
            );

            services.Add(service);
            return service;
        }
    }
}