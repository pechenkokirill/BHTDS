using BHTDS.Engine.Scenes;

namespace BHTDS.Engine.Events;

public sealed class SceneEventBus : EventBus<Scene>
{
    private readonly Queue<Scene> startEvents = [];

    public void EnqueueStart(Scene item)
    {
        this.startEvents.Enqueue(item);
    }


    public override void OnStart()
    {
        while (this.startEvents.TryDequeue(out var eventItem))
        {
            StartCallback.Invoke(eventItem);
        }
    }
}