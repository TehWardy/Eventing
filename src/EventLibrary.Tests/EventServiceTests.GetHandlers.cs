using EventLibrary.Tests.TestServices;
using Moq;
using Xunit;

namespace EventLibrary.Tests
{
    public partial class EventServiceTests
    {
        [Fact]
        public async void GetsHandlerInformation()
        {
            // given 
            brokerMock.Setup(broker => broker.GetHandlers("FakeEvent"))
                .Returns(new Func<FakeObject, ValueTask>[] { (i) => ValueTask.CompletedTask });

            // when 
            eventService.ListenToEvent("FakeEvent", (i) => { return ValueTask.CompletedTask; });
            await eventService.RaiseEventAsync("FakeEvent", new FakeObject());

            // then
            brokerMock.Verify(broker =>
                broker.ListenToEvent("FakeEvent", It.IsAny<Func<FakeObject, ValueTask>>()),
                    Times.Once);

            brokerMock.Verify(broker =>
                broker.GetHandlers("FakeEvent"),
                    Times.Once);

            brokerMock.VerifyNoOtherCalls();
        }
    }
}