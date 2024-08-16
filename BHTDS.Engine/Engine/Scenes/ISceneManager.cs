using BHTDS.Engine.Core;
using BHTDS.Engine.Scenes;

namespace BHTDS.Engine.Engine.Scenes;
public interface ISceneManager : IFeatureMarker
{
    Scene CreateScene(string sceneName);

    Scene? FindScene(string sceneName);

    void LoadScene(Scene scene);

    void Update();

    void Render();
}
