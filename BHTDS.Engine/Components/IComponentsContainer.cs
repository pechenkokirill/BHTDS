namespace BHTDS.Engine.Components;
public interface IComponentsContainer : IEnumerable<Component>
{
    T? Get<T>() where T : Component;

    T Add<T>(T component) where T : Component;

    Component? Remove<T>() where T : Component;

    IEnumerable<T> GetAll<T>() where T : Component;
}
