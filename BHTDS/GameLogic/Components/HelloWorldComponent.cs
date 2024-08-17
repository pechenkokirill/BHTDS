using BHTDS.Engine.Components.BuildIn;
using BHTDS.Engine.Features;
using BHTDS.Engine.Resources;
using BHTDS.Engine.Resources.BuildIn;

namespace BHTDS.GameLogic.Components;

public sealed class HelloWorldComponent : SceneEntityRefComponent
{
    private Time time = null!;

    protected override void OnAttach()
    {
        var window = Scene.Features.Get<WindowFeature>().Feature;
        window.Size = new OpenTK.Mathematics.Vector2i(800, 800);

        this.time = Scene.Resources.GetTime();
    }

    public override void OnStart()
    {
        Console.WriteLine($"Hello World from scene {Scene.Name}!");
    }

    public override void OnUpdate()
    {
        Console.WriteLine($"Current Delta: {this.time.DeltaTime}");
    }

    public override void OnDestroy()
    {
        Console.WriteLine("Dead!");
    }
}
