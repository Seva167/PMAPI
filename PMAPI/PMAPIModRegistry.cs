using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI
{
    public static class PMAPIModRegistry
    {
        internal static HashSet<MelonMod> loadedMods = new();

        /// <summary>
        /// Inits mod in PMAPI
        /// </summary>
        /// <param name="mod">Mod to register</param>
        public static void InitPMAPI(MelonMod mod)
        {
            loadedMods.Add(mod);
        }
    }
}
