using EventLibrary.Tests.TestServices;
using Moq;
using Xunit;

namespace EventLibrary.Tests
{
    public partial class EventServiceTests
    {
        [Fact]
        public void ListensToEvents()
        {
            // when 
            eventService.ListenToEvent("FakeEvent", (i) => ValueTask.CompletedTask);

            // then
            brokerMock.Verify(broker =>
                broker.ListenToEvent("FakeEvent", It.IsAny<Func<FakeObject, ValueTask>>()),
                Times.Once);

            brokerMock.VerifyNoOtherCalls();
        }
    }
}