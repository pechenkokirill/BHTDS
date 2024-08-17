using OpenTK.Windowing.Desktop;

namespace BHTDS.Engine.Features;

public sealed class WindowFeature : FeatureAdapter<NativeWindow>
{
    public WindowFeature(NativeWindow service) : base(service)
    {
    }
}
