using JetBrains.Application.BuildScript.Application.Zones;

namespace ReSharperPlugin.GenericsInlayHints
{
    [ZoneDefinition]
     [ZoneDefinitionConfigurableFeature("Title", "Description", IsInProductSection: false)]
    public interface IMyTestPluginZone : IZone
    {
    }
}
