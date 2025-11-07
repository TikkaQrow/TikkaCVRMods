using MelonLoader;
using red.sim.LightVolumesUdon.Properties;
using System.Diagnostics;
using System.Reflection;

[assembly: MelonInfo(
    typeof(red.sim.LightVolumesUdon.Main),
    nameof(red.sim.LightVolumesUdon),
    AssemblyInfoParams.Version,
    AssemblyInfoParams.Author,
    downloadLink: "https://github.com/TikkaQrow/TikkaCVRMods/releases/download/v1.0.1/red.sim.LightVolumesUdon.dll"
)]

[assembly: MelonGame(null, "ChilloutVR")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default |
                      DebuggableAttribute.DebuggingModes.DisableOptimizations |
                      DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints |
                      DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: MelonPlatform(MelonPlatformAttribute.CompatiblePlatforms.WINDOWS_X64)]
[assembly: MelonPlatformDomain(MelonPlatformDomainAttribute.CompatibleDomains.MONO)]
[assembly: MelonColor(255, 255, 111, 255)]
[assembly: MelonAuthorColor(255, 255, 111, 255)]
[assembly: HarmonyDontPatchAll]

namespace red.sim.LightVolumesUdon.Properties
{
    internal static class AssemblyInfoParams
    {
        public const string Version = "1.0.1";
        public const string Author = "REDSIM , TikkaQrow";
    }
}