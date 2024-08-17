using BHTDS.Engine.Events;
using BHTDS.Engine.Features;

namespace BHTDS.Engine.Scenes;

public class SceneManager : ISceneManagerFeature
{
    private readonly IFeatureLocator featureLocator;
    private readonly Dictionary<string, Scene> scenes = [];
    private readonly SceneEventBus eventBus;

    public SceneManager(IFeatureLocator featureLocator)
    {
        this.featureLocator = featureLocator;
        this.eventBus = new SceneEventBus
        {
            StartCallback = s => s.Start(),
            UpdateCallback = s => s.Update(),
            RenderCallback = s => s.Render(),
        };
    }

    public Scene CreateScene(string sceneName)
    {
        var scene = new Scene(this.featureLocator, sceneName);
        this.scenes.Add(sceneName, scene);
        this.eventBus.EnqueueStart(scene);
        return scene;
    }

    public Scene? FindScene(string sceneName) => this.scenes.GetValueOrDefault(sceneName);

    public void Start()
    {
        this.eventBus.OnStart();
    }

    public void Update()
    {
        this.eventBus.EnqueueUpdate(this.scenes.Values);
        this.eventBus.OnUpdate();
    }

    public void Render()
    {
        this.eventBus.EnqueueUpdate(this.scenes.Values);
        this.eventBus.OnRender();
    }
}
