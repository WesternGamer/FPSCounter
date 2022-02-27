using Sandbox.Engine.Physics;
using Sandbox.Engine.Utils;
using Sandbox.Engine;
using Sandbox.Game.Gui;
using Sandbox.Game.Multiplayer;
using Sandbox.Graphics;
using System;
using System.Threading;
using VRageMath;
using FPSCounter.Config;

namespace FPSCounter
{
    internal class FPSOverlay : MyGuiScreenDebugBase //Not using MyGuiScreenBase as they can get removed. MyGuiScreenDebugBase screens get restored after they get removed in this case.
    {
        public static FPSOverlay Instance;

        private readonly Thread DataUpdateThread;
        private PluginConfig Config => Main.Instance.Config;

        private int FPS = 0;
        private float SIMSpeed = 0f;
        private float ServerSIMSpeed = 0f;
        private int Ping = 0;

        public FPSOverlay() : base(new Vector2(0.5f, 0.5f), default(Vector2), null, isTopMostScreen: true)
        {
            m_isTopMostScreen = true;
            m_drawEvenWithoutFocus = true;
            base.CanHaveFocus = false;
            m_canShareInput = false;
            m_canCloseInCloseAllScreenCalls = false;
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

            if (Config.ShowFPS)
            {
                MyGuiManager.DrawString("White", $"FPS: {FPS}", stringPosition, 0.5f, Config.TextColor);
                stringPosition.Y += 0.01f;
            }
            
            if (Config.ShowSS)
            {
                MyGuiManager.DrawString("White", $"SS: {SIMSpeed}", stringPosition, 0.5f, Config.TextColor);
                stringPosition.Y += 0.01f;
            }
            
            if (Config.ShowServerSS)
            {
                MyGuiManager.DrawString("White", $"Server SS: {ServerSIMSpeed}", stringPosition, 0.5f, Config.TextColor);
                stringPosition.Y += 0.01f;
            }
            
            if (Config.ShowPing)
            {
                MyGuiManager.DrawString("White", $"Ping: {Ping}", stringPosition, 0.5f, Config.TextColor);
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
            DataUpdateThread.Abort();
            Instance = null;
        }

        public void Update()
        {
            while (true)
            {
                FPS = MyFpsManager.GetFps();
                SIMSpeed = MyPhysics.SimulationRatio;

                if (Sync.Layer != null)
                {
                    ServerSIMSpeed = Sync.ServerSimulationRatio;
                    Ping = (int)MyGeneralStats.Static.Ping;
                }

                Thread.Sleep(500);
            }
        }
    }
}
