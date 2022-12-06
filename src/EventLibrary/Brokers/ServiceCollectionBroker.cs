using Microsoft.Extensions.DependencyInjection;

namespace EventLibrary.Services
{
    public class ServiceCollectionBroker : IServiceCollectionBroker
    {
        readonly IServiceCollection serviceCollection;

        public ServiceCollectionBroker(IServiceCollection serviceCollection) =>
            this.serviceCollection = serviceCollection;

        public IServiceCollection GetServiceCollection() =>
            serviceCollection;
    }
}