namespace BHTDS.Engine.Core;

public interface IFeatureMarker;

public interface IFeatureLocator
{
    T Get<T>() where T : IFeatureMarker;
}
