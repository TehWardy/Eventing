namespace EventLibrary.Services.Orchestration.Interfaces
{
    public interface IEventOrchestrationService<T>
    {
        void ListenToEvent(string name, Func<T, ValueTask> handler);
        ValueTask RaiseEventAsync(string name, T data);
    }
}