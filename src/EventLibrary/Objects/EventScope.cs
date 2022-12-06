using EventLibrary.Objects.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EventLibrary.Objects
{
    internal class EventScope<T> : IEventScope<T>
    {
        public IServiceScope ServiceScope { get; }

        public EventMessage<T> Message { get; }

        internal EventScope(IServiceScope serviceScope, EventMessage<T> message)
        {
            ServiceScope = serviceScope;
            Message = message;
        }
    }
}
