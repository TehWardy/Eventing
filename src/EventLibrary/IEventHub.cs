namespace EventLibrary
{
    public interface IEventHub
    {
        void ListenToEvent<T>(string name, Func<T, ValueTask> handler);
        ValueTask RaiseEventAsync<T>(string name, T data);
    }
}