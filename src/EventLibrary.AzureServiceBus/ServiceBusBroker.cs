using Azure.Messaging.ServiceBus;
using EventLibrary.AzureServiceBus.Interfaces;
using EventLibrary.Objects;

namespace EventLibrary.AzureServiceBus
{
    public class AzureServiceBusClient : IAzureServiceBusCient
    {
        readonly ServiceBusClient serviceBusClient;

        public AzureServiceBusClient(ServiceBusClient serviceBusClient) =>
            this.serviceBusClient = serviceBusClient;

        public async ValueTask RaiseEventAsync<T>(string name, EventMessage<T> eventMessage)
        {
            var message = new ServiceBusMessage()
            {
                Body = new BinaryData(eventMessage),
                MessageId = $"{eventMessage.AuthInfo.SSOUserId}_{typeof(T).Name}_{Guid.NewGuid()}" 
            };

            var sender = serviceBusClient.CreateSender(name);
            await sender.SendMessageAsync(message);
            await sender.DisposeAsync();
        }
    }
}