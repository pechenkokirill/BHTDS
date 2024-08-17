namespace BHTDS.Engine.Features;

public interface IFeatureMarker;

public interface IFeatureLocator
{
    T Get<T>() where T : IFeatureMarker;
}
