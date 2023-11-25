using PMAPI.CustomSubstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PMAPI
{
    public static class ExtDataManager
    {
        internal static ExtData extData = new();

        /// <summary>
        /// Sets custom mod data that is stored in save file
        /// </summary>
        /// <param name="data">Mod data</param>
        public static void SetData<T>(T data)
        {
            var modID = Assembly.GetCallingAssembly().GetCustomAttribute<PMAPIModAttribute>().id;

            var json = JsonSerializer.Serialize(data);

            if(!extData.ModData.ContainsKey(modID))
                extData.ModData.Add(modID, json);
            else
                extData.ModData[modID] = json;
        }

        /// <summary>
        /// Gets custom mod data that is stored in save file
        /// </summary>
        /// <returns>Mod data</returns>
        public static T GetData<T>()
        {
            var modID = Assembly.GetCallingAssembly().GetCustomAttribute<PMAPIModAttribute>().id;

            if (extData.ModData.TryGetValue(modID, out var data))
                return JsonSerializer.Deserialize<T>(data);

            return default;
        }

        [Serializable]
        public class ExtData
        {
            public Dictionary<string, string> ModData { get; set; } = new();
            public HashSet<CubeEIDEntry> EidLocTable { get; set; } = new();

            [Serializable]
            public class CubeEIDEntry
            {
                public string EID { get; set; }
                public int ChunkIndex { get; set; }
                public int GroupIndex { get; set; }
                public int CubeIndex { get; set; }
            }
        }
    }
}
