using BHTDS.Engine.Entities;
using BHTDS.Engine.Scenes;

namespace BHTDS.Engine.Components;
public abstract class Component
{
    public virtual void OnAttach(Scene scene, Entity entity) { }

    public virtual void OnEnable() { }

    public virtual void OnDisable() { }

    public virtual void OnDetach() { }

    public virtual void OnDestroy() { }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void OnRender() { }
}
