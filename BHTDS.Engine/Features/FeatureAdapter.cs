namespace BHTDS.Engine.Features;

public abstract class FeatureAdapter<T> : IFeatureMarker
{
    public FeatureAdapter(T service)
    {
        Feature = service;
    }

    public T Feature { get; }
}
