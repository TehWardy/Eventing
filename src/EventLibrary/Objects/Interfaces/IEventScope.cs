using Microsoft.Extensions.DependencyInjection;

namespace EventLibrary.Objects.Interfaces
{
    public interface IEventScope
    {
        IServiceScope ServiceScope { get; }
        IEventAuthInfo AuthInfo { get; }
    }
}