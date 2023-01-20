using EventLibrary.Objects.Interfaces;

namespace EventLibrary.Objects
{
    public class EventMessage<T>
    {
        public IEventAuthInfo AuthInfo { get; set; }
        public T Data { get; set; }
    }
}