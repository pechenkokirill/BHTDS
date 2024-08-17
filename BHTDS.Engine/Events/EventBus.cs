namespace BHTDS.Engine.Events;

public class EventBus<T>
{
    protected readonly Queue<T> updateEvents = [];

    public required Action<T> StartCallback { get; init; }

    public required Action<T> UpdateCallback { get; init; }

    public required Action<T> RenderCallback { get; init; }


    public void EnqueueUpdate(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            this.updateEvents.Enqueue(item);
        }
    }

    public virtual void OnStart()
    {
        while (this.updateEvents.TryDequeue(out var eventItem))
        {
            StartCallback.Invoke(eventItem);
        }
    }

    public virtual void OnUpdate()
    {
        while (this.updateEvents.TryDequeue(out var eventItem))
        {
            UpdateCallback.Invoke(eventItem);
        }
    }

    public virtual void OnRender()
    {
        while (this.updateEvents.TryDequeue(out var eventItem))
        {
            RenderCallback.Invoke(eventItem);
        }
    }
}
