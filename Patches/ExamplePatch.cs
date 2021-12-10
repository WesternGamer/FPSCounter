using HarmonyLib;
using Sandbox.Graphics;
using SpaceEngineers.Game.GUI;
using VRageMath;

namespace SEPluginTemplate.Patches
{
    /// <summary>
    /// This example patch disables the Space Engineers Logo and DLC icons from showing in the main menu, pause menu, and loading menu. It then writes the position entered in the position argument in the middle of the screen. This example shows what a patch looks like commonly. For more info, please visit: https://harmony.pardeike.net
    /// </summary>
    [HarmonyPatch(typeof(MyBadgeHelper), "DrawGameLogo")]
    internal class ExamplePatch
    {
        /// <summary>
        /// Executes before the original method. Can replace the original method by returning false. 
        /// </summary>
        /// <param name="__instance">"Patches can use an argument named __instance to access the instance value if original method is not static. This is similar to the C# keyword this when used in the original method." From Harmony Docs.</param>
        /// <param name="position">"The name of a given argument (that is to be matched to the argument of the original method) must either be the same name or of the form __n, where n is the zero-based index of the argument in the orignal method (you can use argument annotations to map to custom names). From Harmony Docs."</param>
        /// <returns></returns>
        private static bool Prefix(MyBadgeHelper __instance, Vector2 position)
        {
            //Refreshes the game logo in the same instance as this method.
            __instance.RefreshGameLogo();
            //Draws text about the position argument passed to this method.
            MyGuiManager.DrawString("White", "Logo position: X: " + position.X + " Y: " + position.Y, new Vector2(0.5f, 0.5f), 1f);
            return false;
        }
    }
}
