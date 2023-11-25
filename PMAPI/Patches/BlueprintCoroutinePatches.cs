using HarmonyLib;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI.Patches
{
    [HarmonyPatch(typeof(Blueprint.__c__DisplayClass3_0), nameof(Blueprint.__c__DisplayClass3_0._Save_b__0))]
    internal static class BlueprintSaveRoutinePatch
    {
        private static void Prefix()
        {
            SaveAndLoadCompressPatch.isBlueprint = true;
        }
    }

    [HarmonyPatch(typeof(Blueprint.__c__DisplayClass6_0), nameof(Blueprint.__c__DisplayClass6_0._Parse_b__0))]
    internal static class BlueprintParseRoutinePatch
    {
        private static void Prefix()
        {
            SaveAndLoadDecompressPatch.isBlueprint = true;
        }
    }
}
