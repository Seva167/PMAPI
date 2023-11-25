using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace PMAPI.CustomSubstances.Patches
{
    [HarmonyPatch(typeof(SubstanceManager), nameof(SubstanceManager.GetMaterial))]
    internal static class SubstanceManagerGetMaterialPatch
    {
        private static bool Prefix(ref Material __result, string materialName)
        {
            if (CustomMaterialManager.customMaterials.TryGetValue(materialName, out Material val))
            {
                __result = val;
                return false;
            }

            return true;
        }
    }
}
