using BHTDS.Engine.Components.BuildIn;
using BHTDS.Engine.Core;
using BHTDS.Engine.Entities;
using BHTDS.Engine.Events;
using BHTDS.Engine.Features;
using BHTDS.Engine.Resources;

namespace BHTDS.Engine.Scenes;

public sealed class Scene
{
    private readonly List<Entity> entities = [];
    private readonly TwoWayMap<string, Entity> namedEntityes = new();
    private readonly ComponentEventBus componentsEventBus;
    private readonly EventBus<Entity> entityEventBus;

    public Scene(IFeatureLocator featureLocator, string sceneName)
    {
        Features = featureLocator;
        Name = sceneName;

        this.componentsEventBus = new()
        {
            StartCallback = c => c.OnStart(),
            UpdateCallback = c => c.OnUpdate(),
            RenderCallback = c => c.OnRender(),
            DestroyCallback = c => c.OnDestroy(),
            AttachCallback = (c, e, s) => c.OnAttach(s, e),
        };

        this.entityEventBus = new()
        {
            StartCallback = x => x.Start(),
            UpdateCallback = x => x.Update(),
            RenderCallback = x => x.Render(),
        };
    }
    
    public string Name { get; }

    public IFeatureLocator Features { get; }

    public IResourcesFeature Resources => Features.Get<IResourcesFeature>();

    public IReadOnlyCollection<Entity> Entities => this.entities;

    public IReadOnlyDictionary<string, Entity> NamedEntities => this.namedEntityes.Map;

    public Entity CreateEntity(string? name = default)
    {
        var entity = new Entity(this.componentsEventBus);
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
        this.componentsEventBus.OnAttach(this);
        this.entityEventBus.EnqueueUpdate(entities);
        this.entityEventBus.OnStart();
    }

    public void Update()
    {
        this.componentsEventBus.OnAttach(this);
        this.entityEventBus.EnqueueUpdate(entities);
        this.entityEventBus.OnUpdate();
    }

    public void Render()
    {
        this.entityEventBus.EnqueueUpdate(entities);
        this.entityEventBus.OnRender();
    }
}
