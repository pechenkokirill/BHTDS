using BHTDS.Engine.Core;
using BHTDS.Engine.Features;
using BHTDS.Engine.Rendering;
using BHTDS.Engine.Resources;
using BHTDS.Engine.Resources.BuildIn;
using BHTDS.Engine.Scenes;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BHTDS.Engine;

public class BHTDSEngine : IDisposable
{
    private readonly Time time;
    private readonly DictionaryFeatureContainer featureContainer;
    private readonly NativeWindow window;
    private readonly SceneManager sceneManager;
    private readonly Renderer renderer;

    public BHTDSEngine()
    {
        this.featureContainer = new DictionaryFeatureContainer();
        this.window = new NativeWindow(NativeWindowSettings.Default);
        this.sceneManager = new SceneManager(featureContainer);
        this.time = new Time();
        this.renderer = new Renderer();
    }

    public ISceneManagerFeature SceneManager => this.sceneManager;

    public void Run()
    {
        var resources = new ResourcesFeature();
        resources.Add(this.time);

        this.featureContainer.RegisterFeature(new WindowFeature(this.window));
        this.featureContainer.RegisterFeature<IResourcesFeature>(resources);
        this.featureContainer.RegisterFeature<ISceneManagerFeature>(this.sceneManager);
        this.featureContainer.RegisterFeature<IRendererFeature>(this.renderer);

        this.window.Context.MakeCurrent();

        while (!this.window.IsExiting)
        {
            var beginFrameTime = (float)GLFW.GetTime();

            NativeWindow.ProcessWindowEvents(false);

            this.sceneManager.Start();
            this.sceneManager.Update();
            this.sceneManager.Render();
            this.renderer.DrawFrame();
            this.window.Context.SwapBuffers();
            this.time.DeltaTime = (float)GLFW.GetTime() - beginFrameTime;
        }
    }

    public void Dispose()
    {
        this.window.Dispose();
    }
}
