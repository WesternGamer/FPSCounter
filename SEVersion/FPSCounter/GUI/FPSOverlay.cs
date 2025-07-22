using FPSCounter.Config;
using ParallelTasks;
using Sandbox.Engine;
using Sandbox.Engine.Multiplayer;
using Sandbox.Engine.Physics;
using Sandbox.Engine.Utils;
using Sandbox.Game.Gui;
using Sandbox.Game.Multiplayer;
using Sandbox.Game.World;
using Sandbox.Graphics;
using VRageMath;

namespace FPSCounter.GUI
{
    internal class FPSOverlay : MyGuiScreenDebugBase //Not using MyGuiScreenBase as they can get removed. MyGuiScreenDebugBase screens get restored after they get removed in this case.
    {
        private PluginConfig Config => Main.Instance.Config;

        public FPSOverlay() : base(new Vector2(0.5f, 0.5f), default(Vector2), null, isTopMostScreen: true)
        {
            m_isTopMostScreen = true;
            m_drawEvenWithoutFocus = true;
            base.CanHaveFocus = false;
            m_canShareInput = false;
            m_canCloseInCloseAllScreenCalls = false;
        }

        public override bool Draw()
        {
            if (MyHud.HudState == 0 && MySession.Static != null && Config.HideStatsWithHud)
            {
                return false;
            }

            Vector2 stringPosition = MyGuiManager.ComputeFullscreenGuiCoordinate(VRage.Utils.MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP, 5, 5);
            float scale = Config.Scale;

            if (Config.ShowFPS)
            {
                MyGuiManager.DrawString("White", $"FPS: {Singleton<Data>.Instance.Fps}", stringPosition, scale, Config.TextColor);
                stringPosition.Y += MyGuiManager.MeasureString("White", "a", scale).Y;
            }

            if (MySession.Static == null)
            {
                return true;
            }
            
            if (Config.ShowSS)
            {
                MyGuiManager.DrawString("White", $"SS: {Singleton<Data>.Instance.SimSpeed}", stringPosition, scale, Config.TextColor);
                stringPosition.Y += MyGuiManager.MeasureString("White", "a", scale).Y;
            }

            if (MyMultiplayer.Static == null || MyMultiplayer.Static.IsServer)
            {
                return true;
            }
            
            if (Config.ShowServerSS)
            {
                MyGuiManager.DrawString("White", $"Server SS: {Singleton<Data>.Instance.ServerSimSpeed}", stringPosition, scale, Config.TextColor);
                stringPosition.Y += MyGuiManager.MeasureString("White", "a", scale).Y;
            }
            
            if (Config.ShowPing)
            {
                MyGuiManager.DrawString("White", $"Ping: {Singleton<Data>.Instance.Ping}", stringPosition, scale, Config.TextColor);
            }
            
            return true;
        }
    }
}
