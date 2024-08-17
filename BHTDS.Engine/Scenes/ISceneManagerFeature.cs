using BHTDS.Engine.Features;

namespace BHTDS.Engine.Scenes;
public interface ISceneManagerFeature : IFeatureMarker
{
    Scene CreateScene(string sceneName);

    Scene? FindScene(string sceneName);

    void Update();

    void Render();
}
