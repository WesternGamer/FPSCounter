using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VRageMath;

namespace FPSCounter.Config
{
    public class PluginConfig : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetValue<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;

            OnPropertyChanged(propName);
        }

        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;

            propertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private bool showFPS = true;
        private bool showSS = true;
        private bool showServerSS = true;
        private bool showPing = true;
        private Color textColor = new Color(0, 255, 0, 255);
        private bool hideStatsWithHud = true;
        private float scale = 0.5f;

        public bool ShowFPS
        {
            get => showFPS;
            set => SetValue(ref showFPS, value);
        }

        public bool ShowSS
        {
            get => showSS;
            set => SetValue(ref showSS, value);
        }

        public bool ShowServerSS
        {
            get => showServerSS;
            set => SetValue(ref showServerSS, value);
        }

        public bool ShowPing
        {
            get => showPing;
            set => SetValue(ref showPing, value);
        }

        public Color TextColor
        {
            get => textColor;
            set => SetValue(ref textColor, value);
        }

        public bool HideStatsWithHud
        {
            get => hideStatsWithHud;
            set => SetValue(ref hideStatsWithHud, value);
        }
        public float Scale
        {
            get => scale;
            set => SetValue(ref scale, value);
        }
    }
}