using EventLibrary.Brokers.Interfaces;
using EventLibrary.Objects;
using EventLibrary.Services.Foundation.Interfaces;
using EventLibrary.Services.Processing.Interfaces;

namespace EventLibrary.Services.Processing
{
    public class EventProcessingService<T> : IEventProcessingService<T>
    {
        readonly IEventService<EventMessage<T>> eventService;
        readonly IEventAuthorizationBroker authBroker;

        public EventProcessingService(
            IEventService<EventMessage<T>> eventService,
            IEventAuthorizationBroker authBroker)
        {
            this.eventService = eventService;
            this.authBroker = authBroker;
        }

        public void ListenToEvent(string name, Func<T, ValueTask> handler) =>
            eventService.ListenToEvent(name,
                async (EventMessage<T> message) => 
                    await handler(message.Data));

        public async ValueTask RaiseEventAsync(string name, T data)
        {
            var eventMessage = new EventMessage<T>
            {
                AuthInfo = authBroker.GetEventAuthInfo(),
                Data = data
            };

            await eventService.RaiseEventAsync(name, eventMessage);
        }
    }
}