using BHTDS.Engine.Entities;

namespace BHTDS.Engine.Engine.Scenes;

internal interface ISceneLoader
{
    IEnumerable<Entity> CreateEntities();
}
