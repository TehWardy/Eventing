using Microsoft.Extensions.DependencyInjection;

namespace EventLibrary.Services
{
    public interface IServiceCollectionBroker
    {
        IServiceCollection GetServiceCollection();
    }
}