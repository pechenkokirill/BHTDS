using BHTDS.Engine.Entities;

namespace BHTDS.Engine.Scenes;

internal interface ISceneLoader
{
    IEnumerable<Entity> CreateEntities();
}
