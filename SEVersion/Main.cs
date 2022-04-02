using FPSCounter.Config;
using FPSCounter.GUI;
using FPSCounter.Logging;
using Sandbox.Graphics.GUI;
using System.IO;
using VRage.FileSystem;
using VRage.Plugins;

namespace FPSCounter
{
    public class Main : IPlugin
    {
        public const string Name = "FPSCounter";
        public static Main Instance { get; private set; }

        public PluginLogger Log => Logger;
        private static readonly PluginLogger Logger = new PluginLogger(Name);

        public PluginConfig Config => config?.Data;
        private PersistentConfig<PluginConfig> config;
        private static readonly string ConfigFileName = $"{Name}.cfg";

        private TitlebarStats TitlebarStats { get; set; }

        public void Dispose()
        {
            Instance = null;
            Instance?.Dispose();
        }

        public void Init(object gameInstance)
        {
            Instance = this;

            if (!Directory.Exists(Path.Combine(MyFileSystem.UserDataPath, "Storage\\PluginData")))
            {
                Directory.CreateDirectory(Path.Combine(MyFileSystem.UserDataPath, "Storage\\PluginData"));
            }

            var configPath = Path.Combine(MyFileSystem.UserDataPath, "Storage\\PluginData", ConfigFileName);
            config = PersistentConfig<PluginConfig>.Load(Log, configPath);

            MyGuiSandbox.AddScreen(new FPSOverlay());
            TitlebarStats = new TitlebarStats();
        }

        public void Update()
        {
            TitlebarStats.Instance.Draw();
        }

        public void OpenConfigDialog()
        {
            MyGuiSandbox.AddScreen(new MyPluginConfigDialog());
        }
    }
}
