using Harmony;
using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI.CustomSubstances
{
    public static class EIDManager
    {
        internal static Dictionary<Substance, string> eidDictionary = new();

        internal static void CreateEIDTable(SaveAndLoad.SaveData saveData)
        {
            ExtDataManager.extData.EidLocTable.Clear();
            for (int i = 0; i < saveData.chunks.Count; i++)
            {
                var chunk = saveData.chunks[i];
                for (int j = 0; j < chunk.groups.Count; j++)
                {
                    var group = chunk.groups[j];
                    for (int k = 0; k < group.cubes.Count; k++)
                    {
                        var cube = group.cubes[k];
                        if (cube.substance >= 0)
                            continue;

                        ExtDataManager.extData.EidLocTable.Add(new ExtDataManager.ExtData.CubeEIDEntry
                        {
                            EID = eidDictionary[cube.substance],
                            ChunkIndex = i,
                            GroupIndex = j,
                            CubeIndex = k
                        });
                    }
                }
            }
        }

        internal static HashSet<BlueprintEIDTable> CreateEIDTableBlueprint(Blueprint.BlueprintData blueprintData)
        {
            HashSet<BlueprintEIDTable> table = new();

            for (int i = 0; i < blueprintData.groups.Count; i++)
            {
                var group = blueprintData.groups[i];
                for (int j = 0; j < group.cubes.Count; j++)
                {
                    var cube = group.cubes[j];
                    if (cube.substance >= 0)
                        continue;

                    table.Add(new BlueprintEIDTable
                    {
                        EID = eidDictionary[cube.substance],
                        GroupIndex = i,
                        CubeIndex = j
                    });
                }
            }

            return table;
        }

        internal static void ApplyEIDTable(ref SaveAndLoad.SaveData saveData)
        {
            foreach (var entry in ExtDataManager.extData.EidLocTable)
            {
                try
                {
                    var cube = saveData.chunks[entry.ChunkIndex].groups[entry.GroupIndex].cubes[entry.CubeIndex];

                    // Get value by key bruuuh
                    cube.substance = eidDictionary.First(x => x.Value == entry.EID).Key;
                }
                catch (Exception)
                {
                    MelonLogger.Error("EID binding error: EID {0} doesn't exist", entry.EID);
                    continue;
                }
            }
        }

        internal static void ApplyEIDTableBlueprint(ref Blueprint.BlueprintData blueprintData, HashSet<BlueprintEIDTable> table)
        {
            foreach (var entry in table)
            {
                try
                {
                    var cube = blueprintData.groups[entry.GroupIndex].cubes[entry.CubeIndex];

                    cube.substance = eidDictionary.First(x => x.Value == entry.EID).Key;
                }
                catch (Exception)
                {
                    MelonLogger.Error("EID binding error: EID {0} doesn't exist", entry.EID);
                    continue;
                }
            }
        }

        [Serializable]
        internal class BlueprintEIDTable
        {
            public string EID { get; set; }
            public int GroupIndex { get; set; }
            public int CubeIndex { get; set; }
        }
    }
}
