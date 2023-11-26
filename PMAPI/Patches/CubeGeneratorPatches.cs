using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using PMAPI.CustomSubstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PMAPI.Patches
{
    [HarmonyPatch(typeof(CubeGenerator), nameof(CubeGenerator.GenerateGroup))]
    internal static class CubeGeneratorGenerateSavedChunkPatch
    {
        private static void Postfix(SaveAndLoad.GroupData groupData)
        {
            foreach (var req in CustomSaveManager.loadRequests)
            {
                foreach (var cube in groupData.cubes)
                {
                    if (cube.substance >= 0 || req.done || cube.pos != req.beh.transform.localPosition || cube.substance != req.cubeBase.substance)
                        continue;

                    req.beh.GetType().GetMethod("Load").Invoke(req.beh, new string[] { cube.states[0] });
                    req.done = true;
                }
            }

            CustomSaveManager.loadRequests.Clear();
        }
    }

    [HarmonyPatch(typeof(CubeGenerator), nameof(CubeGenerator.OnLoad))]
    internal static class CubeGeneratorOnLoadPatch
    {
        private static void Postfix()
        {
            GameEvents.CallEvent("OnWorldWasLoaded");
        }
    }
}

namespace PMAPI.OreGen.Patches
{
    [HarmonyPatch(typeof(CubeGenerator), nameof(CubeGenerator.GenerateOre))]
    internal static class CubeGeneratorGenerateOrePatch
    {
        private static void Prefix(ref Substance substance, ref float minSize, ref float maxSize, ref float alpha)
        {
            int randSel = Mathf.RoundToInt(CubeGenerator.chunkRandom.Range(0f, CustomOreManager.customOres.Count - 1));
            var ore = CustomOreManager.customOres[randSel];

            if (CubeGenerator.chunkRandom.Range(0f, 1f) <= ore.chance && ore.substanceOverride.HasFlag(substance))
            {
                substance = ore.targetSubstance;
                minSize = ore.minSize;
                maxSize = ore.maxSize;
                alpha = ore.alpha;
            }
        }
    }
}
