using BHTDS.Engine.Entities;
using BHTDS.Engine.Scenes;

namespace BHTDS.Engine.Components.BuildIn;

public sealed class NameComponent : Component
{
    public string? Name { get; set; }

    public override void OnAttach(Scene scene, Entity entity)
    {
        if (Name is null) return;

        scene.MakeEntityNamed(Name, entity);
    }
}
