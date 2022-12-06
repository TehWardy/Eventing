using Microsoft.Extensions.DependencyInjection;

namespace EventLibrary.Services
{
    public interface IServiceCollectionService
    {
        ServiceCollection GetServiceCollection();
    }
}