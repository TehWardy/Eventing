using EventLibrary.Brokers.Interfaces;
using EventLibrary.Objects;
using EventLibrary.Objects.Interfaces;
using EventLibrary.Services.Orchestration.Interfaces;
using EventLibrary.Services.Processing.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;

namespace EventLibrary.Services
{
    public class EventOrchestrationService<T> : IEventOrchestrationService<T>
    {
        readonly IEventProcessingService<EventMessage<T>> eventProcessingService;
        readonly IServiceCollectionProcessingService serviceCollectionProcessingService;
        readonly IEventAuthorizationBroker authBroker;

        public EventOrchestrationService(
            IEventProcessingService<EventMessage<T>> eventProcessingService,
            IServiceCollectionProcessingService serviceCollectionProcessingService,
            IEventAuthorizationBroker authBroker)
        {
            this.eventProcessingService = eventProcessingService;
            this.serviceCollectionProcessingService = serviceCollectionProcessingService;
            this.authBroker = authBroker;
        }

        public void ListenToEvent(string name, Func<T, ValueTask> handler)
        {
            Type handlingServiceType = GetHandlingServiceType();

            eventProcessingService.ListenToEvent(name,
                async (EventMessage<T> message) =>
                {
                    IServiceProvider serviceProvider = BuildServiceProvider(message.AuthInfo);
                    using IServiceScope scope = serviceProvider.CreateScope();
                    await HandleEvent(handlingServiceType, scope, message, handler);
                });
        }

        public async ValueTask RaiseEventAsync(string name, T data)
        {
            var eventMessage = new EventMessage<T>
            {
                AuthInfo = authBroker.GetEventAuthInfo(),
                Data = data
            };

            await eventProcessingService.RaiseEventAsync(name, eventMessage);
        }

        static Type GetHandlingServiceType()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] frames = stackTrace.GetFrames();

            return frames.Last(f => f.GetMethod().DeclaringType != null)
                .GetMethod()
                .DeclaringType;
        }

        static async ValueTask HandleEvent(
            Type handlingServiceType,
            IServiceScope serviceScope,
            EventMessage<T> message,
            Func<T, ValueTask> handler)
        {
            var handlingService = serviceScope.ServiceProvider
                .GetService(handlingServiceType);

            var eventArgs = new object[] { message.Data };

            ValueTask task = (ValueTask)handler
                .GetMethodInfo()
                .Invoke(handlingService, eventArgs);

            await task;
        }

        IServiceProvider BuildServiceProvider(IEventAuthInfo authInfo)
        {
            var services = serviceCollectionProcessingService
                .GetEventAuthorisedServiceCollection(authInfo);

            return services.BuildServiceProvider();
        }
    }
}