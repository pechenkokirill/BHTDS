using BHTDS.Engine.Components.BuildIn;
using BHTDS.Engine.Core;
using BHTDS.Engine.Entities;
using BHTDS.Engine.Features.Resources;

namespace BHTDS.Engine.Scenes;

public sealed class Scene
{
    private readonly Queue<Entity> updateEntityQueue = [];
    private readonly List<Entity> entities = [];
    private readonly TwoWayMap<string, Entity> namedEntityes = new();

    public Scene(IFeatureLocator featureLocator) => Features = featureLocator;

    public IFeatureLocator Features { get; }

    public IResourcesFeature Resources => Features.Get<IResourcesFeature>();

    public IReadOnlyCollection<Entity> Entities => this.entities;

    public IReadOnlyDictionary<string, Entity> NamedEntities => this.namedEntityes.Map;

    public Entity CreateEntity(string? name = default)
    {
        var entity = new Entity(this);
        this.entities.Add(entity);
        if (name is not null) entity.AddComponent<NameComponent>(x => x.Name = name);
        return entity;
    }

    public void Destroy(Entity entity)
    {
        this.entities.Remove(entity);
        this.namedEntityes.Remove(entity);
        entity.Destroyed();
    }

    public void MakeEntityNamed(string name, Entity entity)
    {
        this.namedEntityes.Add(name, entity);
    }

    public Entity? FindByName(string name) => NamedEntities.GetValueOrDefault(name);

    public void Start()
    {
        foreach (var entity in entities)
        {
            updateEntityQueue.Enqueue(entity);
        }

        while (updateEntityQueue.TryDequeue(out var entity))
        {
            entity.Start();
        }
    }

    public void Update()
    {
        foreach (var entity in entities)
        {
            updateEntityQueue.Enqueue(entity);
        }

        while (updateEntityQueue.TryDequeue(out var entity))
        {
            entity.Update();
        }
    }

    public void Render()
    {
        foreach (var entity in entities)
        {
            updateEntityQueue.Enqueue(entity);
        }

        while (updateEntityQueue.TryDequeue(out var entity))
        {
            entity.Render();
        }
    }
}
