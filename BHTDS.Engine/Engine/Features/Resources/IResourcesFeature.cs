using BHTDS.Engine.Core;
using static BHTDS.Engine.Features.Resources.ResourcesFeature;

namespace BHTDS.Engine.Features.Resources;

public interface IResourcesFeature : IFeatureMarker
{
    void Add<T>(T resource) where T : IResource;

    T Get<T>() where T : IResource;

    void Delete<T>() where T : IResource;
}
