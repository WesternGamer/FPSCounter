using System;
using System.Text;
using Sandbox;
using Sandbox.Graphics.GUI;
using VRage;
using VRage.Utils;
using VRageMath;

namespace FPSCounter.GUI
{
    public class MyPluginConfigDialog : MyGuiScreenBase
    {
        private const string Caption = "FPS Counter Configuration";
        public override string GetFriendlyName() => "MyPluginConfigDialog";

        private MyLayoutTable layoutTable;

        private MyGuiControlLabel showFPSLabel;
        private MyGuiControlCheckbox showFPSCheckbox;
        private MyGuiControlLabel showSSLabel;
        private MyGuiControlCheckbox showSSCheckbox;
        private MyGuiControlLabel showServerSSLabel;
        private MyGuiControlCheckbox showServerSSCheckbox;
        private MyGuiControlLabel showPingLabel;
        private MyGuiControlCheckbox showPingCheckbox;
        private MyGuiControlLabel colorLabel;
        private MyGuiControlColor colorSlider;
        private MyGuiControlLabel hideStatsWithHudLabel;
        private MyGuiControlCheckbox hideStatsWithHudCheckbox;
        private MyGuiControlLabel scaleLabel;
        private MyGuiControlSlider scaleSlider;

        // TODO: Add member variables for your UI controls here

        private MyGuiControlButton closeButton;

        public MyPluginConfigDialog() : base(new Vector2(0.5f, 0.5f), MyGuiConstants.SCREEN_BACKGROUND_COLOR, new Vector2(0.4f, 0.6f), false, null, MySandboxGame.Config.UIBkOpacity, MySandboxGame.Config.UIOpacity)
        {
            EnabledBackgroundFade = true;
            m_closeOnEsc = true;
            m_drawEvenWithoutFocus = true;
            CanHideOthers = true;
            CanBeHidden = true;
            CloseButtonEnabled = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            RecreateControls(true);
        }

        public override void RecreateControls(bool constructor)
        {
            base.RecreateControls(constructor);

            CreateControls();
            LayoutControls();
        }

        private void CreateControls()
        {
            AddCaption(Caption);

            var config = Main.Instance.Config;
            CreateCheckbox(out showFPSLabel, out showFPSCheckbox, config.ShowFPS, value => config.ShowFPS = value, "Show FPS", "Enables FPS counter.");
            CreateCheckbox(out showSSLabel, out showSSCheckbox, config.ShowSS, value => config.ShowSS = value, "Show Sim speed", "Enables Sim speed counter.");
            CreateCheckbox(out showServerSSLabel, out showServerSSCheckbox, config.ShowServerSS, value => config.ShowServerSS = value, "Show server Sim speed", "Enables server Sim speed counter.");
            CreateCheckbox(out showPingLabel, out showPingCheckbox, config.ShowPing, value => config.ShowPing = value, "Show ping", "Enables ping counter.");
            CreateCheckbox(out hideStatsWithHudLabel, out hideStatsWithHudCheckbox, config.HideStatsWithHud, value => config.HideStatsWithHud = value, "Hide stats with HUD", "");
            CreateColorSelector(out colorLabel, out colorSlider, config.TextColor, value => config.TextColor = value, "Text color", "Change the text color.");
            CreateSlider(out scaleLabel, out scaleSlider, config.Scale, value => config.Scale = value, "Scale", "");

            closeButton = new MyGuiControlButton(new Vector2(0f, 0.22f), originAlign: MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP, text: MyTexts.Get(MyCommonTexts.Ok), onButtonClick: OnOk);
        }

        private void OnOk(MyGuiControlButton _) => CloseScreen();

        private void CreateCheckbox(out MyGuiControlLabel labelControl, out MyGuiControlCheckbox checkboxControl, bool value, Action<bool> store, string label, string tooltip)
        {
            labelControl = new MyGuiControlLabel
            {
                Text = label,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };

            checkboxControl = new MyGuiControlCheckbox(toolTip: tooltip)
            {
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                Enabled = true,
                IsChecked = value
            };
            checkboxControl.IsCheckedChanged += cb => store(cb.IsChecked);
        }

        private void CreateColorSelector(out MyGuiControlLabel labelControl, out MyGuiControlColor colorControl, Color value, Action<Color> store, string label, string tooltip)
        {
            labelControl = new MyGuiControlLabel
            {
                Text = label,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };

            colorControl = new MyGuiControlColor("", 0.8f, Vector2.Zero, value, value, MyStringId.GetOrCompute(tooltip))
            {
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                Enabled = true,
            };
            colorControl.OnChange += cb => store(cb.Color);
        }

        private void CreateSlider(out MyGuiControlLabel labelControl, out MyGuiControlSlider sliderControl, float value, Action<float> store, string label, string tooltip)
        {
            labelControl = new MyGuiControlLabel
            {
                Text = label,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };

            sliderControl = new MyGuiControlSlider(Vector2.Zero, defaultValue: value, toolTip: tooltip)
            {
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                Enabled = true,
            };
            sliderControl.ValueChanged += cb => store(cb.Value);
        }

        private void LayoutControls()
        {
            var size = Size ?? Vector2.One;
            layoutTable = new MyLayoutTable(this, new Vector2(-0.16f, -0.22f), 0.55f * size);
            layoutTable.SetColumnWidths(455f, 100f);
            // TODO: Add more row heights here as needed
            layoutTable.SetRowHeights(50f, 50f, 50f, 50f, 50f, 60f, 50f, 60f, 50f);

            var row = 0;

            layoutTable.Add(showFPSLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(showFPSCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(showSSLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(showSSCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(showServerSSLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(showServerSSCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(showPingLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(showPingCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(hideStatsWithHudLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(hideStatsWithHudCheckbox, MyAlignH.Center, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(colorLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            row++;
            layoutTable.Add(colorSlider, MyAlignH.Center, MyAlignV.Center, row, 0, colSpan: 2);
            row++;

            layoutTable.Add(scaleLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            row++;
            layoutTable.Add(scaleSlider, MyAlignH.Center, MyAlignV.Center, row, 0, colSpan: 2);
            row++;

            Controls.Add(closeButton);
            // row++;
        }
    }
}
