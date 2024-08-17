using BHTDS.Engine.Features;
using static BHTDS.Engine.Resources.ResourcesFeature;

namespace BHTDS.Engine.Resources;

public interface IResourcesFeature : IFeatureMarker
{
    void Add<T>(T resource) where T : IResource;

    T Get<T>() where T : IResource;

    void Delete<T>() where T : IResource;
}
