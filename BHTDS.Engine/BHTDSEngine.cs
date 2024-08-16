using BHTDS.Engine.Core;
using BHTDS.Engine.Features;
using BHTDS.Engine.Features.Resources;
using BHTDS.Engine.Features.Resources.BuildIn;
using BHTDS.Engine.Scenes;
using OpenTK.Windowing.Desktop;

namespace BHTDS;

public class BHTDSEngine : IDisposable
{
    private readonly Time time;
    private readonly DictionaryFeatureContainer featureContainer;
    private readonly NativeWindow window;

    public BHTDSEngine()
    {
        this.featureContainer = new DictionaryFeatureContainer();
        this.window = new NativeWindow(NativeWindowSettings.Default);
        this.time = new Time();
    }

    public void Run()
    {
        var resources = new ResourcesFeature();
        resources.Add(this.time);

        this.featureContainer.RegisterFeature(new WindowFeature(this.window));
        this.featureContainer.RegisterFeature<IResourcesFeature>(resources);

        this.window.Context.MakeCurrent();

        var scene = new Scene(this.featureContainer);
        scene.Start();

        while (!this.window.IsExiting)
        {
            NativeWindow.ProcessWindowEvents(false);

            scene.Update();

            scene.Render();

            this.window.Context.SwapBuffers();
        }
    }

    public void Dispose()
    {
        this.window.Dispose();
    }
}
