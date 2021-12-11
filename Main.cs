using Sandbox.Graphics.GUI;
using VRage.Plugins;

namespace FPSCounter
{
    public class Main : IPlugin
    {
        public void Dispose()
        {

        }

        public void Init(object gameInstance)
        {
            MyGuiSandbox.AddScreen(new FPSOverlay());
        }

        public void Update()
        {

        }
    }
}
