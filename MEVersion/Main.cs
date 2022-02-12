using MEPluginLoader.PluginInterface;
using Sandbox.Graphics.GUI;

namespace FPSCounter
{
    public class Main : IPlugin
    {
        public void Dispose()
        {

        }

        public void Init()
        {
            MyGuiSandbox.AddScreen(new FPSOverlay());
        }

        public void Update()
        {

        }
    }
}
