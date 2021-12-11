using Sandbox.Engine.Physics;
using Sandbox.Engine.Utils;
using Sandbox.Game.Gui;
using Sandbox.Game.Multiplayer;
using Sandbox.Graphics;
using System;
using VRageMath;

namespace FPSCounter
{
    internal class FPSOverlay : MyGuiScreenDebugBase //Not using MyGuiScreenBase as they can get removed. MyGuiScreenDebugBase screens get restored after they get removed in this case.
    {
        public static FPSOverlay Instance;
        public FPSOverlay() : base(new Vector2(0.5f, 0.5f), default(Vector2), null, isTopMostScreen: true)
        {
            m_isTopMostScreen = true;
            m_drawEvenWithoutFocus = true;
            base.CanHaveFocus = false;
            m_canShareInput = false;
            Instance = this;
        }

        public override bool Draw()
        {
            Vector2 stringPosition = MyGuiManager.ComputeFullscreenGuiCoordinate(VRage.Utils.MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP, 5, 5);
            MyGuiManager.DrawString("White", "FPS: " + Convert.ToString(MyFpsManager.GetFps()), stringPosition, 0.5f, new Color(0, 255, 0, 255));
            stringPosition.Y += 0.01f;
            MyGuiManager.DrawString("White", "SS: " + Convert.ToString(MyPhysics.SimulationRatio), stringPosition, 0.5f, new Color(0, 255, 0, 255));
            stringPosition.Y += 0.01f;
            if (Sync.Layer != null)
            {
                MyGuiManager.DrawString("White", "Server SS: " + Convert.ToString(Sync.ServerSimulationRatio), stringPosition, 0.5f, new Color(0, 255, 0, 255));
            }
            return base.Draw();
        }

        public override string GetFriendlyName()
        {
            return "FPSOverlay";
        }

        public override void UnloadData()
        {
            base.UnloadData();
            Instance = null;
        }
    }
}
