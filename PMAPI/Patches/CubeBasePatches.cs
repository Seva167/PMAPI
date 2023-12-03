using HarmonyLib;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI.CustomSubstances.Patches
{
    [HarmonyPatch(typeof(CubeBase), nameof(CubeBase.UpdateSubstanceBehavior))]
    internal static class CubeBaseUpdateSubstanceBehaviourPatch
    {
        private static void Postfix(CubeBase __instance)
        {
            if (CustomSubstanceManager.customParams.TryGetValue(__instance.substance, out CustomSubstanceParams val))
            {
                if (val.behInit != null)
                    __instance.substanceBehavior = val.behInit.Invoke(__instance);
            }
        }
    }

    [HarmonyPatch(typeof(CubeBase), nameof(CubeBase.Initialize))]
    internal static class CubeBaseInitializePatch
    {
        private static void Postfix(CubeBase __instance)
        {
            __instance.gameObject.SendMessage("OnInitialize");
        }
    }
}
