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

        // TODO: Add member variables for your UI controls here

        private MyGuiControlMultilineText infoText;
        private MyGuiControlButton closeButton;

        public MyPluginConfigDialog() : base(new Vector2(0.5f, 0.5f), MyGuiConstants.SCREEN_BACKGROUND_COLOR, new Vector2(0.5f, 0.7f), false, null, MySandboxGame.Config.UIBkOpacity, MySandboxGame.Config.UIOpacity)
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
            CreateColorSelector(out colorLabel, out colorSlider, config.TextColor, value => config.TextColor = value, "Text color", "Change the text color.");
            // TODO: Create your UI controls here

            infoText = new MyGuiControlMultilineText
            {
                Name = "InfoText",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                TextAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                TextBoxAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                // TODO: Add 2 short lines of text here if the player needs to know something. Ask for feedback here. Etc.
                Text = new StringBuilder("\r\nTODO")
            };

            closeButton = new MyGuiControlButton(new Vector2(0f, 0.2f), originAlign: MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP, text: MyTexts.Get(MyCommonTexts.Ok), onButtonClick: OnOk);
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

            colorControl = new MyGuiControlColor(label, 0.8f, Vector2.Zero, value, value, MyStringId.GetOrCompute(tooltip))
            {
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                Enabled = true,
            };
            colorControl.OnChange += cb => store(cb.Color);
        }

        private void LayoutControls()
        {
            var size = Size ?? Vector2.One;
            layoutTable = new MyLayoutTable(this, new Vector2(-0.22f, -0.28f), 0.55f * size);
            layoutTable.SetColumnWidths(450f, 100f);
            // TODO: Add more row heights here as needed
            layoutTable.SetRowHeights(90f, 90f, 90f, 90f, 90f, 150f);

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

            layoutTable.Add(colorLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(colorSlider, MyAlignH.Center, MyAlignV.Center, row, 1);
            row++;

            // TODO: Layout your UI controls here

            layoutTable.Add(infoText, MyAlignH.Left, MyAlignV.Top, row, 0, colSpan: 2);
            row++;

            Controls.Add(closeButton);
            // row++;
        }
    }
}
