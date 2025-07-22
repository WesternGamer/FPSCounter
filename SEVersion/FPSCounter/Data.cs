using ParallelTasks;
using Sandbox.Engine;
using Sandbox.Engine.Physics;
using Sandbox.Engine.Utils;
using Sandbox.Game.Multiplayer;
using System.Threading;

namespace FPSCounter
{
    internal class Data : Singleton<Data>
    {
        private readonly Thread DataUpdateThread;

        public int Fps { get; private set; } = default;
        public float SimSpeed { get; private set; } = default;
        public float ServerSimSpeed { get; private set; } = default;
        public int Ping { get; private set; } = default;

        public Data()
        {
            DataUpdateThread = new Thread(new ThreadStart(UpdateStats))
            {
                IsBackground = true
            };
            DataUpdateThread.Start();
        }

        private void UpdateStats()
        {
            while (true)
            {
                Fps = MyFpsManager.GetFps();
                SimSpeed = MyPhysics.SimulationRatio;
                ServerSimSpeed = Sync.ServerSimulationRatio;
                Ping = (int)MyGeneralStats.Static.Ping;
                Thread.Sleep(500);
            }
        }
    }
}
