using HarmonyLib;
using Il2Cpp;
using Il2CppSystem.Diagnostics;
using MelonLoader;
using PMAPI.CustomSubstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEngine;

namespace PMAPI.Patches
{
    [HarmonyPatch(typeof(SaveAndLoad), nameof(SaveAndLoad.Compress))]
    internal static class SaveAndLoadCompressPatch
    {
        internal static bool isBlueprint = false;

        private static void Prefix(ref string str)
        {
            string extJson;
            if (isBlueprint)
            {
                var bluepringData = JsonUtility.FromJson<Blueprint.BlueprintData>(str);
                var table = EIDManager.CreateEIDTableBlueprint(bluepringData);
                extJson = JsonSerializer.Serialize(table);
            }
            else
            {
                var saveData = JsonUtility.FromJson<SaveAndLoad.SaveData>(str);
                EIDManager.CreateEIDTable(saveData);
                extJson = JsonSerializer.Serialize(ExtDataManager.extData);
            }

            StringBuilder sb = new();
            sb.Append("#PMAPI_EXTDAT");
            sb.Append((21 + extJson.Length).ToString("X8"));
            sb.Append(extJson);
            sb.Append(str);
            str = sb.ToString();

            isBlueprint = false;
        }
    }

    [HarmonyPatch(typeof(SaveAndLoad), nameof(SaveAndLoad.Decompress))]
    internal static class SaveAndLoadDecompressPatch
    {
        internal static bool isBlueprint = false;

        private static void Postfix(ref string __result)
        {
            if (__result[0] != '#')
                return;

            int stopIndex = int.Parse(__result.Substring(13, 8), System.Globalization.NumberStyles.HexNumber);
            string extJson = __result[21..stopIndex];

            __result = __result[stopIndex..];

            if (isBlueprint)
            {
                var table = JsonSerializer.Deserialize<HashSet<EIDManager.BlueprintEIDTable>>(extJson);
                var blueprintData = JsonUtility.FromJson<Blueprint.BlueprintData>(__result);
                EIDManager.ApplyEIDTableBlueprint(ref blueprintData, table);

                __result = JsonUtility.ToJson(blueprintData);
            }
            else
            {
                ExtDataManager.extData = JsonSerializer.Deserialize<ExtDataManager.ExtData>(extJson);
                var saveData = JsonUtility.FromJson<SaveAndLoad.SaveData>(__result);
                EIDManager.ApplyEIDTable(ref saveData);

                __result = JsonUtility.ToJson(saveData);
            }

            isBlueprint = false;
        }
    }
}
