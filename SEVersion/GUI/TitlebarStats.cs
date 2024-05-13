using FPSCounter.Config;
using HarmonyLib;
using Sandbox.Engine;
using Sandbox.Engine.Physics;
using Sandbox.Engine.Utils;
using Sandbox.Game;
using Sandbox.Game.Multiplayer;
using System;
using System.Threading;
using System.Windows.Forms;
using VRage;

namespace FPSCounter.GUI
{
    internal class TitlebarStats : IDisposable
    {
        public static TitlebarStats Instance;

        private Form MainWindow;
        private static PluginConfig Config => Main.Instance.Config;

        private int FPS = 0;
        private float SIMSpeed = 0f;
        private float ServerSIMSpeed = 0f;
        private int Ping = 0;

        private readonly Thread DataUpdateThread;

        public TitlebarStats()
        {
            Instance = this;
            DataUpdateThread = new Thread(new ThreadStart(Update))
            {
                IsBackground = true
            };
            DataUpdateThread.Start();
            MainWindow = (Form)AccessTools.Field(MyVRage.Platform.Windows.GetType(), "m_form").GetValue(MyVRage.Platform.Windows);
        }

        public void UpdateTitleBar()
        {
            string text = "";

            text += MyPerGameSettings.BasicGameInfo.GameName;

            if (Config.ShowFPS)
            {
                text += $" - FPS: {FPS}";
            }

            if (Config.ShowSS)
            {
                text += $" - SS: {SIMSpeed}";
            }

            if (Config.ShowServerSS)
            {
                text += $" - Server SS: {ServerSIMSpeed}";
            }

            if (Config.ShowPing)
            {
                text += $" - Ping: {Ping}";
            }

            MainWindow.Text = text;
        }

        private void Update()
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

                UpdateTitleBar();

                Thread.Sleep(500);
            }
        }

        public void Dispose()
        {
            if (DataUpdateThread != null)
            {
                DataUpdateThread.Abort();
            }
        }
    }
}
