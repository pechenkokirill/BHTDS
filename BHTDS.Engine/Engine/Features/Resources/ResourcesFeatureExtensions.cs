﻿using BHTDS.Engine.Core;
using BHTDS.Engine.Features.Resources.BuildIn;

namespace BHTDS.Engine.Features.Resources;
public static class ResourcesFeatureExtensions
{
    public static T GetResource<T>(this IFeatureLocator locator)
        where T : ResourcesFeature.IResource
    {
        return locator.Get<IResourcesFeature>().Get<T>();
    }

    public static Time GetTime(this IResourcesFeature resources) => resources.Get<Time>();
}
