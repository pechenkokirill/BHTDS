using BHTDS.Engine.Features;

namespace BHTDS.Engine.Core;

public sealed class DictionaryFeatureContainer : IFeatureLocator
{
    private readonly Dictionary<Type, IFeatureMarker> features = [];

    public T Get<T>() where T : IFeatureMarker
    {
        return (T)this.features[typeof(T)];
    }

    public void RegisterFeature<T>(T feature) where T : IFeatureMarker
    {
        this.features.Add(typeof(T), feature);
    }
}
