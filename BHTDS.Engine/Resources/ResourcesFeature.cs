namespace BHTDS.Engine.Resources;

public sealed class ResourcesFeature : IResourcesFeature
{
    public interface IResource;

    private readonly Dictionary<Type, IResource> resources = [];

    public void Add<T>(T resource) where T : IResource
    {
        this.resources.Add(resource.GetType(), resource);
    }

    public T Get<T>() where T : IResource
    {
        return (T)this.resources[typeof(T)];
    }

    public void Delete<T>() where T : IResource
    {
        this.resources.Remove(typeof(T));
    }
}
