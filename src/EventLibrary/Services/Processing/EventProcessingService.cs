using EventLibrary.Brokers.Interfaces;
using EventLibrary.Objects;
using EventLibrary.Objects.Interfaces;
using EventLibrary.Services.Foundation.Interfaces;
using EventLibrary.Services.Processing.Interfaces;

namespace EventLibrary.Services.Processing
{
    public class EventProcessingService<T> : IEventProcessingService<T>
    {
        readonly IEventService<EventMessage<T>> eventService;
        readonly Func<IEventAuthInfo> getAuthInfo;

        public EventProcessingService(IEventService<EventMessage<T>> eventService, Func<IEventAuthInfo> getAuthInfo)
        {
            this.eventService = eventService;
            this.getAuthInfo = getAuthInfo;
        }

        public void ListenToEvent(string name, Func<T, ValueTask> handler) =>
            eventService.ListenToEvent(name,
                async (EventMessage<T> message) => 
                    await handler(message.Data));

        public async ValueTask RaiseEventAsync(string name, T data)
        {
            var eventMessage = new EventMessage<T>
            {
                AuthInfo = getAuthInfo(),
                Data = data
            };

            await eventService.RaiseEventAsync(name, eventMessage);
        }
    }
}