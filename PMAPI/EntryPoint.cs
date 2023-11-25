using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using PMAPI.CustomSubstances;

namespace PMAPI
{
    internal class EntryPoint : MelonMod
    {
        public override void OnInitializeMelon()
        {
            CustomMaterialManager.Init();
            CustomSubstanceManager.Init();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            CustomLocalizer.Reload();
        }
    }
}