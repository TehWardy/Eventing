using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EventLibrary.Services
{
    public class ServiceCollectionService : IServiceCollectionService
    {
        ServiceCollectionBroker serviceCollectionBroker;

        public ServiceCollectionService(ServiceCollectionBroker serviceCollectionBroker) =>
            this.serviceCollectionBroker = serviceCollectionBroker;

        public ServiceCollection GetServiceCollection()
        {
            var result = new ServiceCollection();

            var rootServiceCollection = serviceCollectionBroker.GetServiceCollection();

            foreach (var service in rootServiceCollection)
                result.Add(service);

            return result;
        }
    }
}