using BHTDS.Engine.Core;
using BHTDS.Engine.Engine.Scenes;
using BHTDS.Engine.Features;
using BHTDS.Engine.Features.Resources;
using BHTDS.Engine.Features.Resources.BuildIn;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BHTDS;

public class BHTDSEngine : IDisposable
{
    private readonly Time time;
    private readonly DictionaryFeatureContainer featureContainer;
    private readonly NativeWindow window;
    private readonly SceneManager sceneManager;

    public BHTDSEngine()
    {
        this.featureContainer = new DictionaryFeatureContainer();
        this.window = new NativeWindow(NativeWindowSettings.Default);
        this.sceneManager = new SceneManager(featureContainer);
        this.time = new Time();
    }

    public ISceneManager SceneManager => this.sceneManager;

    public void Run()
    {
        var resources = new ResourcesFeature();
        resources.Add(this.time);

        this.featureContainer.RegisterFeature(new WindowFeature(this.window));
        this.featureContainer.RegisterFeature<IResourcesFeature>(resources);
        this.featureContainer.RegisterFeature<ISceneManager>(this.sceneManager);

        this.window.Context.MakeCurrent();

        while (!this.window.IsExiting)
        {
            var beginFrameTime = (float)GLFW.GetTime();

            NativeWindow.ProcessWindowEvents(false);

            this.sceneManager.Start();

            this.sceneManager.Update();

            this.sceneManager.Render();

            this.window.Context.SwapBuffers();

            this.time.DeltaTime = (float)GLFW.GetTime() - beginFrameTime;
        }
    }

    public void Dispose()
    {
        this.window.Dispose();
    }
}
