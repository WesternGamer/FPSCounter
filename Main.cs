using HarmonyLib;
using System.Reflection;
using VRage.Plugins;

namespace SEPluginTemplate
{
    public class Main : IPlugin
    {
        /// <summary>
        /// Called on startup when plugin loads. Constructor, optional, is not required.
        /// </summary>
        public Main()
        {

        }

        /// <summary>
        /// Called when the game closes. This method is required regardless if you are using it or not. If you are not using it, then just don't put anything in the method.
        /// </summary>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// Called on startup when plugin gets initialized. This method is required regardless if you are using it or not. If you are not using it, then just don't put anything in the method.
        /// </summary>
        public void Init(object gameInstance)
        {
            //Use the code below in this method if you are planning on using Harmony to modify existing game methods. Optional, is not required. For more info about how harmony works, go to https://harmony.pardeike.net/
            // Starts an instance of Harmony
            Harmony harmony = new Harmony("SEPluginTemplate");
            // Patches all patches in the plugin.
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Called every game update. This method is required regardless if you are using it or not. If you are not using it, then just don't put anything in the method.
        /// </summary>
        public void Update()
        {
            
        }
    }
}
