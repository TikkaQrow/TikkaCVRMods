using ABI_RC.Core.Util.AssetFiltering;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Reflection;
using VRCLightVolumes;

namespace red.sim.LightVolumesUdon
{
    public class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Type[] typesToAdd = new Type[]
            {
                typeof(LightVolumeInstance),
                typeof(LightVolumeManager),
                typeof(PointLightVolumeInstance),
                typeof(LightVolumeAudioLink),
                typeof(LightVolumeTVGI)
            };

            FieldInfo volumetricFogField = typeof(WorldFilter).GetField("_VolumetricFogAndMist", BindingFlags.NonPublic | BindingFlags.Static);

            if (volumetricFogField != null)
            {
                HashSet<Type> volumetricFogSet = (HashSet<Type>)volumetricFogField.GetValue(null);

                foreach (Type type in typesToAdd)
                {
                    volumetricFogSet.Add(type);
                }
                //Rainbow Logs make the console a happier place <3
                MelonLogger.Msg(System.ConsoleColor.Red, "LET ");
                MelonLogger.Msg(System.ConsoleColor.Green, "THERE ");
                MelonLogger.Msg(System.ConsoleColor.Blue, "BE ");
                MelonLogger.Msg(System.ConsoleColor.Magenta, "LIGHT!");
            }
            else
            {
                MelonLogger.Error("It's broken. Guess you'll have sit in the dark. Buried in FogAndMist");
            }
        }
    }
}