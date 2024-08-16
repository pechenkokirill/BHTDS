using BHTDS.Engine.Components;
using BHTDS.Engine.Entities;
using BHTDS.Engine.Scenes;

namespace BHTDS.Engine.Engine.Events;

using AttachEvent = (Entity Entity, Component Component);

public class ComponentEventBus : EventBus<Component>
{
    private readonly Queue<AttachEvent> attachQueue = [];

    public required Action<Component, Entity, Scene> AttachCallback { get; init; }

    public void EnqueueAttach(Entity entity, Component component)
    {
        this.attachQueue.Enqueue((entity, component));
    }

    public void OnAttach(Scene scene)
    {
        while (this.attachQueue.TryDequeue(out var eventItem))
        {
            AttachCallback.Invoke(eventItem.Component, eventItem.Entity, scene);
        }
    }
}
