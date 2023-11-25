using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Il2Cpp;
using MelonLoader;

namespace PMAPI.CustomSubstances.Patches
{
    [HarmonyPatch(typeof(SubstanceManager), nameof(SubstanceManager.GetParameter))]
    internal static class SubstanceManagerGetParameterPatch
    {
        private static bool Prefix(ref SubstanceParameters.Param __result, Substance substance)
        {
            if (substance >= 0)
                return true;

            if (CustomSubstanceManager.customSubstances.TryGetValue(substance, out SubstanceParameters.Param val))
            {
                __result = val;
                return false;
            }

            __result = CustomSubstanceManager.errorSubParams;
            return false;
        }
    }
}
