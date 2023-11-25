using MelonLoader;
using Il2Cpp;
using PMAPI;
using UnityEngine;
using Il2CppInterop.Runtime.Injection;
using PMAPI.CustomSubstances;
using PMAPI.OreGen;
using UnityEngine.SceneManagement;
using System.Text.Json;

namespace TestMod
{
    public class TestMod : MelonMod
    {
        // Mod data class
        public class OurData
        {
            public int Test { get; set; }

            public OurData(int test)
            {
                Test = test;
            }
        }

        Substance blud;
        Substance rood;

        public override void OnInitializeMelon()
        {
            // Init PMAPI
            PMAPIModRegistry.InitPMAPI(this);

            // Register in our behaviour in IL2CPP so the game knows about it
            ClassInjector.RegisterTypeInIl2Cpp<TestBeh>(new RegisterTypeOptions
            {
                Interfaces = new[] { typeof(ICubeBehavior), typeof(ISavable) }
            });

            // Registering modded substances
            RegisterBlud();
            RegisterRood();

            // Registering our substances in ore generation
            CustomOreManager.RegisterCustomOre(blud, new CustomOreManager.CustomOreParams
            {
                chance = 0.1f,
                substanceOverride = Substance.Stone,
                maxSize = 0.3f,
                minSize = 0.3f,
                alpha = 1f
            });
            CustomOreManager.RegisterCustomOre(rood, new CustomOreManager.CustomOreParams
            {
                chance = 0.1f,
                substanceOverride = Substance.Stone | Substance.MoonRock,
                maxSize = 0.3f,
                minSize = 0.3f,
                alpha = 1f
            });
        }

        // Gets called just when the world was loaded
        public void OnWorldWasLoaded()
        {
            // Outputting mod data. The question mark means that nothing will be outputted if mod data doesn't exist (data == null)
            MelonLogger.Msg("Mod data is {0}", ExtDataManager.GetData<OurData>()?.Test);
        }

        // Omg!!1!!11 Blue wood leaf
        void RegisterBlud()
        {
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("Wood"))
            {
                name = "Blud",
                color = Color.blue
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.Leaf).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_BLUD";
            param.material = cmat.name;

            // Registering our substance as custom substance
            blud = CustomSubstanceManager.RegisterSubstance("blud", param, new CustomSubstanceParams
            {
                enName = "Blud",
                jpName = "name2",
                behInit = (cb) =>
                {
                    // Adding test behavior
                    var beh = cb.gameObject.AddComponent<TestBeh>();
                    return beh;
                }
            });
        }

        // Red wood bruh
        void RegisterRood()
        {
            Material cmat = new(SubstanceManager.GetMaterial("Wood"))
            {
                name = "Rood",
                color = Color.red
            };
            CustomMaterialManager.RegisterMaterial(cmat);

            var param = SubstanceManager.GetParameter(Substance.Wood).MemberwiseClone().Cast<SubstanceParameters.Param>();
            param.displayNameKey = "SUB_ROOD";
            param.material = cmat.name;
            rood = CustomSubstanceManager.RegisterSubstance("rood", param, new CustomSubstanceParams
            {
                enName = "Rood",
                jpName = "name3"
            });
        }

        public override void OnUpdate()
        {
            // Spawning blud above our head
            if (Input.GetKeyDown(KeyCode.L))
            {
                // Getting player position
                var mv = GameObject.Find("XR Origin").GetComponent<PlayerMovement>();

                // Generating the cube
                CubeGenerator.GenerateCube(mv.cameraTransform.position + new Vector3(0f, 10f, 1f), Vector3.one, blud);
            }

            // Writing test data that will be stored in save file
            if (Input.GetKeyDown(KeyCode.X))
                ExtDataManager.SetData(new OurData(UnityEngine.Random.RandomRangeInt(1, 100)));
        }
    }

    public class TestBeh : MonoBehaviour
    {
        public TestBeh(IntPtr ptr) : base(ptr)
        {
            // Requesting load of cube save data
            CustomSaveManager.RequestLoad(this);
        }

        void Start()
        {
            // Get the cube base
            var cubeBase = GetComponent<CubeBase>();

            // Make it hooooooot
            cubeBase.heat.AddHeat(10000f);
        }

        // Saved variable
        Data data = new("test123");

        // When cube is going to save it's data return the data that we want to save
        public string Save()
        {
            // Serialize into JSON
            return JsonSerializer.Serialize(data);
        }

        // When cube is loaded set our variable to loaded data
        public void Load(string json)
        {
            // From JSON to Data
            data = JsonSerializer.Deserialize<Data>(json);
        }

        public class Data
        {
            public string SomeString { get; set; }

            public Data(string someString)
            {
                SomeString = someString;
            }
        }
    }
}