namespace BHTDS.Engine.Engine.Events;

public class EventBus<T>
{
    protected readonly Queue<T> updateQueue = [];
    protected readonly Queue<T> startQueue = [];

    public required Action<T> StartCallback { get; init; }

    public required Action<T> UpdateCallback { get; init; }

    public required Action<T> RenderCallback { get; init; }

    public required Action<T> DestroyCallback { get; init; }

    public void EnqueueUpdate(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            this.updateQueue.Enqueue(item);
        }
    }

    public void OnStart()
    {
        while (this.startQueue.TryDequeue(out var eventItem))
        {
            StartCallback.Invoke(eventItem);
        }
    }

    public void OnUpdate()
    {
        while (this.updateQueue.TryDequeue(out var eventItem))
        {
            UpdateCallback.Invoke(eventItem);
        }
    }

    public void OnRender()
    {
        while (this.updateQueue.TryDequeue(out var eventItem))
        {
            RenderCallback.Invoke(eventItem);
        }
    }

    internal void OnDestroy()
    {
        while (this.updateQueue.TryDequeue(out var eventItem))
        {
            DestroyCallback.Invoke(eventItem);
        }
    }
}
