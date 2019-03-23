using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class MainGameUI : UIWindow
    {
        private GameModel model;

        public MainGameUI(GameModel model)
        {
            this.model = model;

            var box = new TextBox
            {
                Size = new Size(100, 100),
                Font = new FontSettings { FontFamily = EuropeFontAtlas.GetAtlas(), FontSize = 24 },
                Text = "HelloWorld",
                Location = new Point(0, 0)
            };

            Controls.Add(box);

            
        }

        public override void Unsubscribe()
        {
            throw new System.NotImplementedException();
        }

        public override void OnKeyPress(object sender, KeyEventArgs args)
        {
            model.OnKeyPress(sender, args);
        }

        public override void OnKeyRelease(object sender, KeyEventArgs args)
        {
            model.OnKeyRelease(sender, args);
        }

        public override void OnMouseWheel(object sender, MouseEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public override void OnMouseMove(object sender, MouseEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public override void OnMouseClick(object sender, MouseEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public override void OnMouseDoubleClick(object sender, MouseEventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public override void OnSizeChanged(object sender, EventArgs args)
        {
            Size = ((MainForm)sender).Size;
            model.GameBasicCamera.Size = ((MainForm)sender).Size;
        }
    }
}