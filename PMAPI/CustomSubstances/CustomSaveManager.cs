using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PMAPI.CustomSubstances
{
    public static class CustomSaveManager
    {
        internal static List<LoadRequest> loadRequests = new();

        /// <summary>
        /// Requests loading of behavior save data
        /// </summary>
        /// <param name="beh">Behavior requesting to load it's data</param>
        public static void RequestLoad(MonoBehaviour beh)
        {
            loadRequests.Add(new LoadRequest
            {
                beh = beh,
                cubeBase = beh.gameObject.GetComponent<CubeBase>()
            });
        }

        internal class LoadRequest
        {
            public MonoBehaviour beh;
            public CubeBase cubeBase;
            internal bool done = false;
        }
    }
}
