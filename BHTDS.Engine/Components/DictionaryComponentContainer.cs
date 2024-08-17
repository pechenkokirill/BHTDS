using System.Collections;

namespace BHTDS.Engine.Components;
public sealed class DictionaryComponentContainer : IComponentsContainer
{
    private readonly Dictionary<Type, Component> container = [];

    public T Add<T>(T component) where T : Component
    {
        this.container.Add(typeof(T), component);
        return component;
    }

    public T? Get<T>() where T : Component
    {
        return this.container.GetValueOrDefault(typeof(T)) as T;
    }

    public IEnumerable<T> GetAll<T>() where T : Component
    {
        return this.container.Values.OfType<T>();
    }

    public IEnumerator<Component> GetEnumerator()
    {
        return this.container.Values.GetEnumerator();
    }

    public Component? Remove<T>() where T : Component
    {
        var key = typeof(T);
        if(!this.container.TryGetValue(key, out var component))
        {
            return default;
        }

        this.container.Remove(key);
        return component;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
