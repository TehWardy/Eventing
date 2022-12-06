namespace EventLibrary.Services.Processing.Interfaces
{
    public interface IEventProcessingService<T>
    {
        void ListenToEvent(string name, Func<T, ValueTask> handler);
        ValueTask RaiseEventAsync(string name, T data);
    }
}