using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PMAPI.CustomSubstances
{
    public static class CustomSubstanceManager
    {
        internal static void Init()
        {
            // Warm up the manager so it loads everything properly
            SubstanceManager.GetParameter(Substance.Stone);

            CreateErrorSubstance();
        }

        internal static Dictionary<Substance, SubstanceParameters.Param> customSubstances = new();
        internal static Dictionary<Substance, CustomSubstanceParams> customParams = new();
        internal static SubstanceParameters.Param errorSubParams;

        private static int id = -1;

        /// <summary>
        /// Registers custom substance in game
        /// </summary>
        /// <param name="eid">Extended ID</param>
        /// <param name="substanceParams">Parameters used by the game</param>
        /// <param name="cParams">Parameters used by PMAPI</param>
        /// <returns>ID of registered substance used by the game</returns>
        public static Substance RegisterSubstance(string eid, SubstanceParameters.Param substanceParams, CustomSubstanceParams cParams)
        {
            customSubstances.Add((Substance)id, substanceParams);
            customParams.Add((Substance)id, cParams);

            var modAttrib = Assembly.GetCallingAssembly().GetCustomAttribute<PMAPIModAttribute>();
            EIDManager.eidDictionary.Add((Substance)id, $"{modAttrib.id}:{eid}");

            CustomLocalizer.AddEnString(substanceParams.displayNameKey, cParams.enName);
            CustomLocalizer.AddJpString(substanceParams.displayNameKey, cParams.jpName);

            MelonLogger.Msg("Substance registered: " + EIDManager.eidDictionary[(Substance)id]);
            id--;
            return (Substance)id + 1;
        }

        private static void CreateErrorSubstance()
        {
            var defStone = SubstanceManager.GetParameter(Substance.Stone);
            errorSubParams = defStone.MemberwiseClone().Cast<SubstanceParameters.Param>();

            Material errorMat = Material.GetDefaultMaterial();
            errorMat.shader = Shader.FindBuiltin("Hidden/InternalErrorShader");
            CustomMaterialManager.RegisterMaterial(errorMat);
            errorSubParams.material = errorMat.name;
            errorSubParams.displayNameKey = "SUB_PMAPI_MODERROR";

            CustomLocalizer.AddEnString(errorSubParams.displayNameKey, "Mod error");
            CustomLocalizer.AddJpString(errorSubParams.displayNameKey, "Moddu erroru");
        }

        public static Substance GetSubstanceByEID(string eid) => EIDManager.eidDictionary.FirstOrDefault(x => x.Value == eid).Key;

        public static string GetEIDBySubstance(Substance substance) => EIDManager.eidDictionary[substance];
    }

    /// <summary>
    /// Substance parameters used by PMAPI
    /// </summary>
    public class CustomSubstanceParams
    {
        /// <summary>
        /// Name in English
        /// </summary>
        public string enName;

        /// <summary>
        /// Name in Japanese
        /// </summary>
        public string jpName;

        /// <summary>
        /// Delegate that is called whenever object is initalizing its script
        /// </summary>
        public Func<CubeBase, Component> behInit;
    }
}
