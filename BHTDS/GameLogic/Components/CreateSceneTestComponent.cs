using BHTDS.Engine.Components.BuildIn;
using BHTDS.Engine.Scenes;

namespace BHTDS.GameLogic.Components;

public class CreateSceneTestComponent : SceneEntityRefComponent
{
    private ISceneManagerFeature sceneManager = null!;

    protected override void OnAttach()
    {
        this.sceneManager = Scene.Features.Get<ISceneManagerFeature>();
    }

    public override void OnStart()
    {
        var scene = this.sceneManager.CreateScene("new.test.scene");
        scene.CreateEntity("new.test.entity").AddComponent<HelloWorldComponent>();
    }
}