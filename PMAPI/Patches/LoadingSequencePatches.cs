using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI.Patches
{
    [HarmonyPatch(typeof(LoadingSequence), nameof(LoadingSequence.StartLoading))]
    internal static class LoadingSequenceStartLoadingPatch
    {
        private static void Postfix()
        {
            ExtDataManager.extData = new();
            GameEvents.CallEvent("OnLoadingStarted");
        }
    }
}
