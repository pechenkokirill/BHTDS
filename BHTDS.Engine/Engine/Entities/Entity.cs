using BHTDS.Engine.Components;
using BHTDS.Engine.Engine.Events;

namespace BHTDS.Engine.Entities;

public sealed class Entity
{
    private readonly IComponentsContainer components = new DictionaryComponentContainer();
    private readonly ComponentEventBus eventBus;

    public Entity(ComponentEventBus eventBus)
    {
        this.eventBus = eventBus;
    }

    public T AddComponent<T>(Action<T>? configure = default) where T : Component
    {
        var component = Activator.CreateInstance<T>();
        configure?.Invoke(component);
        return AddComponent(component);
    }

    private T AddComponent<T>(T component) where T : Component
    {
        this.components.Add(component);
        this.eventBus.EnqueueAttach(this, component);
        return component;
    }

    public void RemoveComponent<T>() where T : Component =>
        this.components.Remove<T>()?.OnDetach();

    public T? GetComponent<T>() where T : Component => 
        this.components.Get<T>();

    public void Start()
    {
        this.eventBus.EnqueueUpdate(this.components);
        this.eventBus.OnStart();
    }

    public void Update()
    {
        this.eventBus.EnqueueUpdate(this.components);
        this.eventBus.OnUpdate();
    }

    public void Render()
    {
        this.eventBus.EnqueueUpdate(this.components);
        this.eventBus.OnRender();
    }

    public void Destroyed()
    {
        this.eventBus.EnqueueUpdate(this.components);
        this.eventBus.OnDestroy();
    }
}
