using EventLibrary.Services.Orchestration.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EventLibrary
{
    public class EventHub : IEventHub
    {
        readonly IServiceProvider serviceProvider;
        readonly List<object> services = new();

        public EventHub(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            services = new List<object>();
        }

        public void ListenToEvent<T>(string name, Func<T, ValueTask> handler) => 
            GetEventService<T>().ListenToEvent(name, handler);

        public async ValueTask RaiseEventAsync<T>(string name, T data) =>
            await GetEventService<T>().RaiseEventAsync(name, data);

        IEventOrchestrationService<T> GetEventService<T>() =>
            services.FirstOrDefault(s => s.GetType().GenericTypeArguments[0] == typeof(T)) as IEventOrchestrationService<T>
                    ??
                CreateEventService<T>();

        IEventOrchestrationService<T> CreateEventService<T>()
        {
            var service = serviceProvider.GetService<IEventOrchestrationService<T>>();
            services.Add(service);
            return service;
        }
    }
}