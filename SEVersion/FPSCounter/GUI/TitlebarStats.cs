using FPSCounter.Config;
using HarmonyLib;
using ParallelTasks;
using Sandbox.Engine.Multiplayer;
using Sandbox.Game;
using Sandbox.Game.World;
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
            string text = GetTitleBarString();

            if (MainWindow != null)
            {
                MainWindow.Invoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    MainWindow.Text = text;
                });
            }
        }

        public string GetTitleBarString()
        {
            string text = "";

            text += MyPerGameSettings.BasicGameInfo.GameName;

            if (Config.ShowFPS)
            {
                text += $" - FPS: {Singleton<Data>.Instance.Fps}";
            }

            if (MySession.Static == null)
            {
                return text;
            }

            if (Config.ShowSS)
            {
                text += $" - SS: {Singleton<Data>.Instance.SimSpeed}";
            }

            if (MyMultiplayer.Static == null || MyMultiplayer.Static.IsServer)
            {
                return text;
            }

            if (Config.ShowServerSS)
            {
                text += $" - Server SS: {Singleton<Data>.Instance.ServerSimSpeed}";
            }

            if (Config.ShowPing)
            {
                text += $" - Ping: {Singleton<Data>.Instance.Ping}";
            }

            return text;
        }

        private void Update()
        {
            while (true)
            {
                if (MainWindow.Disposing || MainWindow.IsDisposed)
                {
                    return;
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
