using EventLibrary.Brokers.Interfaces;
using EventLibrary.Objects.Interfaces;

namespace EventLibrary
{
    public class EventAuthorizationBroker : IEventAuthorizationBroker
    {
        IEventAuthInfo authInfo;

        public EventAuthorizationBroker(IEventAuthInfo authInfo) =>
            this.authInfo = authInfo;

        public IEventAuthInfo GetEventAuthInfo() =>
            authInfo;
    }
}