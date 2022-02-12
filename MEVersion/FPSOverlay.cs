using Sandbox.Engine.Physics;
using Sandbox.Engine.Utils;
using Sandbox.Game.Gui;
using Sandbox.Game.Multiplayer;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using Sandbox.Gui.Utility;
using System;
using System.Threading;
using VRageMath;

namespace FPSCounter
{
    internal class FPSOverlay : MyGuiScreenDebugBase //Not using MyGuiScreenBase as they can get removed. MyGuiScreenDebugBase screens get restored after they get removed in this case.
    {
        public static FPSOverlay Instance;

        private readonly Thread DataUpdateThread;

        private int FPS = 0;
        private float SIMSpeed = 0f;
        private float ServerSIMSpeed = 0f;

        public FPSOverlay() : base(new Vector2(0.5f, 0.5f), default(Vector2), null, isTopMostScreen: true)
        {
            m_isTopMostScreen = true;
		    m_drawEvenWithoutFocus = true;
		    CanHaveFocus = false;
		    m_canShareInput = false;
		    m_grabInputFocus = false;
            m_isTopScreen = true;
            m_canCloseInCloseAllScreenCalls = false;
            m_closeOnEsc = false;
            CanBeHidden = false;
            Instance = this;
            DataUpdateThread = new Thread(new ThreadStart(Update))
            {
                IsBackground = true
            };
            DataUpdateThread.Start();
        }

        public override bool Draw()
        {
            Vector2 stringPosition = MyGuiManager.ComputeFullscreenGuiCoordinate(VRage.Utils.MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP, 5, 5);

            MyFontHelper.DrawString(MyGuiConstants.DEFAULT_FONT, $"FPS: {FPS}", stringPosition, 0.55f, new Color(0, 255, 0, 255));
            stringPosition.Y += 0.015f;

            MyFontHelper.DrawString(MyGuiConstants.DEFAULT_FONT, $"SS: {SIMSpeed}", stringPosition, 0.55f, new Color(0, 255, 0, 255));
            stringPosition.Y += 0.015f;

            MyFontHelper.DrawString(MyGuiConstants.DEFAULT_FONT, $"Server SS: {ServerSIMSpeed}", stringPosition, 0.55f, new Color(0, 255, 0, 255));

            return base.Draw();
        }

        public override string GetFriendlyName()
        {
            return "FPSOverlay";
        }

        public override void UnloadData()
        {
            base.UnloadData();
            DataUpdateThread.Abort();
            Instance = null;
        }

        public void Update()
        {
            while (true)
            {
                FPS = MyFpsManager.GetFps();
                SIMSpeed = MyPhysicsSandbox.SimulationRatio;

                if (Sync.Layer != null)
                    ServerSIMSpeed = Sync.ServerSimulationRatio;

                Thread.Sleep(500);
            }
        }
    }
}
