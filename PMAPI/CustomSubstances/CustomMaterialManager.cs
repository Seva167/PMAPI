using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PMAPI.CustomSubstances
{
    public static class CustomMaterialManager
    {
        internal static void Init()
        {
            // Warm up the manager so it loads everything properly
            SubstanceManager.GetMaterial("Stone");
        }

        internal static Dictionary<string, Material> customMaterials = new();

        /// <summary>
        /// Registers custom material in game for use in custom substances
        /// </summary>
        /// <param name="mat">Unity Engine material</param>
        public static void RegisterMaterial(Material mat)
        {
            customMaterials.Add(mat.name, mat);
        }
    }
}
