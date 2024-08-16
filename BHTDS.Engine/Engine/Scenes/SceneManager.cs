using BHTDS.Engine.Core;
using BHTDS.Engine.Scenes;

namespace BHTDS.Engine.Engine.Scenes;
public class SceneManager : ISceneManager
{
    private readonly IFeatureLocator featureLocator;
    private readonly Dictionary<string, Scene> scenes = [];
    private readonly List<Scene> sceneUpdateList = [];
    private readonly Queue<Scene> sceneUpdateQueue = [];
    private readonly Queue<Scene> sceneStartQueue = [];

    public SceneManager(IFeatureLocator featureLocator)
    {
        this.featureLocator = featureLocator;
    }

    public Scene CreateScene(string sceneName)
    {
        var scene = new Scene(this.featureLocator, sceneName);
        this.scenes.Add(sceneName, scene);
        return scene;
    }

    public Scene? FindScene(string sceneName) => this.scenes.GetValueOrDefault(sceneName);

    public void LoadScene(Scene scene)
    {
        this.sceneStartQueue.Enqueue(scene);
    }

    public void Start()
    {
        while (this.sceneStartQueue.TryDequeue(out var scene))
        {
            this.sceneUpdateList.Add(scene);
            scene.Start();
        }
    }

    public void Update()
    {
        foreach (var scene in this.scenes.Values)
        {
            this.sceneUpdateQueue.Enqueue(scene);
        }

        while (this.sceneUpdateQueue.TryDequeue(out var scene))
        {
            scene.Update();
        }
    }

    public void Render()
    {
        foreach (var scene in this.scenes.Values)
        {
            this.sceneUpdateQueue.Enqueue(scene);
        }

        while (this.sceneUpdateQueue.TryDequeue(out var scene))
        {
            scene.Render();
        }
    }
}
