using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI.OreGen
{
    public static class CustomOreManager
    {
        internal static List<CustomOreParams> customOres = new();

        /// <summary>
        /// Registers substance in custom ore generator
        /// </summary>
        /// <param name="substance">Substance that is registered</param>
        /// <param name="customOreParams">Parameters used by custom ore generator</param>
        public static void RegisterCustomOre(Substance substance, CustomOreParams customOreParams)
        {
            customOreParams.targetSubstance = substance;
            customOres.Add(customOreParams);
        }

        public class CustomOreParams
        {
            public Substance targetSubstance;

            /// <summary>
            /// Chance of generating
            /// </summary>
            public float chance;

            /// <summary>
            /// Which substance to override (can be multiple: Substance.Stone | Substance.MoonRock)
            /// </summary>
            public Substance substanceOverride;

            /// <summary>
            /// Minimum size of cube
            /// </summary>
            public float minSize;

            /// <summary>
            /// Maximum size of cube
            /// </summary>
            public float maxSize;

            /// <summary>
            /// TBD
            /// </summary>
            public float alpha;
        }
    }
}
