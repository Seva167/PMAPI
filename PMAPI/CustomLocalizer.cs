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

        static Dictionary<string, string> zhHansDictionary = new();
        static Dictionary<string, string> deDictionary = new();
        static Dictionary<string, string> esDictionary = new();
        static Dictionary<string, string> frDictionary = new();
        static Dictionary<string, string> ruDictionary = new();

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

                case "StringTable_zh-Hans":
                    ReloadTable(zhHansDictionary);
                    break;
                case "StringTable_de":
                    ReloadTable(deDictionary);
                    break;
                case "StringTable_es":
                    ReloadTable(esDictionary);
                    break;
                case "StringTable_fr":
                    ReloadTable(frDictionary);
                    break;
                case "StringTable_ru":
                    ReloadTable(ruDictionary);
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

        /// <summary>
        /// Adds entry to Simplified Chinese localization table
        /// </summary>
        /// <param name="key">Localization displayNameKey (SUB_TEST)</param>
        /// <param name="value">The text which is displayed to player</param>
        public static void AddZhHansString(string key, string value)
        {
            zhHansDictionary.Add(key, value);
        }

        /// <summary>
        /// Adds entry to German localization table
        /// </summary>
        /// <param name="key">Localization displayNameKey (SUB_TEST)</param>
        /// <param name="value">The text which is displayed to player</param>
        public static void AddDeString(string key, string value)
        {
            deDictionary.Add(key, value);
        }

        /// <summary>
        /// Adds entry to Spanish localization table
        /// </summary>
        /// <param name="key">Localization displayNameKey (SUB_TEST)</param>
        /// <param name="value">The text which is displayed to player</param>
        public static void AddEsString(string key, string value)
        {
            esDictionary.Add(key, value);
        }

        /// <summary>
        /// Adds entry to French localization table
        /// </summary>
        /// <param name="key">Localization displayNameKey (SUB_TEST)</param>
        /// <param name="value">The text which is displayed to player</param>
        public static void AddFrString(string key, string value)
        {
            frDictionary.Add(key, value);
        }

        /// <summary>
        /// Adds entry to Russian localization table
        /// </summary>
        /// <param name="key">Localization displayNameKey (SUB_TEST)</param>
        /// <param name="value">The text which is displayed to player</param>
        public static void AddRuString(string key, string value)
        {
            ruDictionary.Add(key, value);
        }
    }
}
