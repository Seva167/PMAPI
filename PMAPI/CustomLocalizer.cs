using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI
{
    internal static class CustomLocalizer
    {
        static Dictionary<string, string> enDictionary = new();
        static Dictionary<string, string> jpDictionary = new();

        /// <summary>
        /// Reload localization
        /// </summary>
        public static void Reload()
        {
            switch (Localizer.table.name)
            {
                case "StringTable_en":
                    ReloadTable(enDictionary);
                    break;
                case "StringTable_ja":
                    ReloadTable(jpDictionary);
                    break;
            }
        }

        private static void ReloadTable(Dictionary<string, string> cTable)
        {
            foreach (var pair in cTable)
            {
                Localizer.table.RemoveEntry(pair.Key);
                Localizer.table.AddEntry(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Adds entry to English localization table
        /// </summary>
        /// <param name="key">Localization displayNameKey (SUB_TEST)</param>
        /// <param name="value">The text which is displayed to player</param>
        public static void AddEnString(string key, string value)
        {
            enDictionary.Add(key, value);
        }

        /// <summary>
        /// Adds entry to Japanese localization table
        /// </summary>
        /// <param name="key">Localization displayNameKey (SUB_TEST)</param>
        /// <param name="value">The text which is displayed to player</param>
        public static void AddJpString(string key, string value)
        {
            jpDictionary.Add(key, value);
        }
    }
}
