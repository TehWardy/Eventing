using EventLibrary.Brokers.Interfaces;
using EventLibrary.Objects;
using EventLibrary.Services.Foundation.Interfaces;
using EventLibrary.Services.Processing;
using EventLibrary.Services.Processing.Interfaces;
using EventLibrary.Tests.TestServices;
using Moq;

namespace EventLibrary.Tests
{
    public class EventProcessingServiceTests
    {
        readonly Mock<IEventService<EventMessage<FakeObject>>> eventServiceMock;
        readonly Mock<IEventAuthorizationBroker> authBrokerMock;
        readonly IEventProcessingService<FakeObject> eventProcessingService;

        public EventProcessingServiceTests()
        {
            eventServiceMock = new Mock<IEventService<EventMessage<FakeObject>>>();
            authBrokerMock = new Mock<IEventAuthorizationBroker>();

            eventProcessingService = 
                new EventProcessingService<FakeObject>(
                    eventServiceMock.Object, 
                    authBrokerMock.Object.GetEventAuthInfo);
        }
    }
}