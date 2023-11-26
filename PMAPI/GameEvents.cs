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
        internal static void CallEvent(string eventName)
        {
            foreach (var mod in PMAPIModRegistry.loadedMods)
            {
                try
                {
                    mod.SendMessage(eventName);
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
