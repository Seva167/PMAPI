using HarmonyLib;
using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI.Patches
{
    [HarmonyPatch(typeof(Localizer), nameof(Localizer.OnLocaleChanged))]
    internal static class LocalizerOnLocaleChangedPatch
    {
        private static void Postfix()
        {
            CustomLocalizer.Reload();
        }
    }
}
