using EventLibrary.Objects.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EventLibrary.Services
{
    public class ServiceCollectionProcessingService : IServiceCollectionProcessingService
    {
        ServiceCollectionService serviceCollectionService;

        public ServiceCollectionProcessingService(ServiceCollectionService serviceCollectionService) =>
            this.serviceCollectionService = serviceCollectionService;

        public ServiceCollection GetEventAuthorisedServiceCollection(IEventAuthInfo authInfo)
        {
            var result = new ServiceCollection();
            var rootServiceCollection = serviceCollectionService.GetServiceCollection();

            foreach (var service in rootServiceCollection)
                result.Add(service);

            result.AddScoped((serviceProvider) => authInfo);
            return result;
        }
    }
}