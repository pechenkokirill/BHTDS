using BHTDS.Engine.Entities;
using BHTDS.Engine.Scenes;

namespace BHTDS.Engine.Components.BuildIn;
public abstract class SceneEntityRefComponent : Component
{
    public Entity Entity { get; private set; } = null!;

    public Scene Scene { get; private set; } = null!;

    public sealed override void OnAttach(Scene scene, Entity entity)
    {
        Scene = scene;
        Entity = entity;
        OnAttach();
    }

    protected virtual void OnAttach() { }

    public void DestroySelf() => Scene.Destroy(Entity);
}
