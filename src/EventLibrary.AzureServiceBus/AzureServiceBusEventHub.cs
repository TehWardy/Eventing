using EventLibrary.AzureServiceBus.Interfaces;
using EventLibrary.Objects;
using EventLibrary.Objects.Interfaces;

namespace EventLibrary.AzureServiceBus
{
    public class AzureServiceBusEventHub
    {
        readonly Func<IEventAuthInfo> getAuthInfo;
        readonly IAzureServiceBusCient serviceBusClient;

        public AzureServiceBusEventHub(
            Func<IEventAuthInfo> getAuthInfo,
            IAzureServiceBusCient serviceBusClient)
        {
            this.getAuthInfo = getAuthInfo;
            this.serviceBusClient = serviceBusClient;
        }

        // we currently don't use this as
        // we intend to use azure functions with SB triggers on them
        public void ListenToEvent<T>(string name, Func<T, ValueTask> handler) =>
            throw new NotImplementedException();

        public async ValueTask RaiseEventAsync<T>(string name, T data)
        {
            var eventMessage = new EventMessage<T>
            {
                AuthInfo = getAuthInfo(),
                Data = data
            };

            await serviceBusClient.RaiseEventAsync<T>(name, eventMessage);
        }
    }
}
