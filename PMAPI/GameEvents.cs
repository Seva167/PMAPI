using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMAPI
{
    internal static class GameEvents
    {
        internal static void CallOnWorldWasLoaded()
        {
            foreach (var mod in PMAPIModRegistry.loadedMods)
            {
                try
                {
                    mod.SendMessage("OnWorldWasLoaded");
                }
                catch (Exception e)
                {
                    MelonLogger.Error(e.Message);
                    continue;
                }
            }
        }
    }
}
