using BHTDS.Engine.Components;
using BHTDS.Engine.Scenes;

namespace BHTDS.Engine.Entities;

public sealed class Entity
{
    private readonly Queue<Component> componentUpdateQueue = [];
    private readonly IComponentsContainer components = new DictionaryComponentContainer();
    private readonly Scene scene;

    public Entity(Scene scene)
    {
        this.scene = scene;
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
        component.OnAttach(this.scene, this);
        return component;
    }

    public void RemoveComponent<T>() where T : Component =>
        this.components.Remove<T>()?.OnDetach();

    public T? GetComponent<T>() where T : Component => 
        this.components.Get<T>();

    public void Start()
    {
        foreach (var component in components)
        {
            componentUpdateQueue.Enqueue(component);
        }

        while (componentUpdateQueue.TryDequeue(out var component))
        {
            component.OnStart();
        }
    }

    public void Update()
    {
        foreach (var component in components)
        {
            componentUpdateQueue.Enqueue(component);
        }

        while (componentUpdateQueue.TryDequeue(out var component))
        {
            component.OnUpdate();
        }
    }

    public void Render()
    {
        foreach (var component in components)
        {
            componentUpdateQueue.Enqueue(component);
        }

        while (componentUpdateQueue.TryDequeue(out var component))
        {
            component.OnRender();
        }
    }

    public void Destroyed()
    {
        foreach (var component in components)
        {
            componentUpdateQueue.Enqueue(component);
        }

        while (componentUpdateQueue.TryDequeue(out var component))
        {
            component.OnDestroy();
        }
    }
}
