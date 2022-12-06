using EventLibrary.Objects.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EventLibrary.Services
{
    public interface IServiceCollectionProcessingService
    {
        ServiceCollection GetEventAuthorisedServiceCollection(IEventAuthInfo authInfo);
    }
}