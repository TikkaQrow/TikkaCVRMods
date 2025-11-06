using RedSimLightVolumes.Properties;
using MelonLoader;
using System.Reflection;

[assembly: MelonInfo(
    typeof(RedSimLightVolumes.Main),
    nameof(RedSimLightVolumes),
    AssemblyInfoParams.Version,
    AssemblyInfoParams.Author,
    downloadLink: ""
)]

[assembly: MelonGame(null, "ChilloutVR")]
[assembly: MelonPlatform(MelonPlatformAttribute.CompatiblePlatforms.WINDOWS_X64)]
[assembly: MelonPlatformDomain(MelonPlatformDomainAttribute.CompatibleDomains.MONO)]
[assembly: MelonColor(255, 3, 252, 78)]
[assembly: MelonAuthorColor(255, 40, 144, 209)] 
[assembly: HarmonyDontPatchAll]

namespace RedSimLightVolumes.Properties
{
    internal static class AssemblyInfoParams
    {
        public const string Version = "1.0.1";
        public const string Author = "REDSIM , SketchFoxsky, TikkaQrow";
    }
}