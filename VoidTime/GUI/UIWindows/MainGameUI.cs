using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class MainGameUI : UIWindow
    {
        private GameModel model;

        public MainGameUI(GameModel model)
        {
            this.model = model;
            Controls.ItemAdded += Controls_ItemAdded;
            Controls.ItemRemoved += Controls_ItemRemoved;


            var box = new TextBox
            {
                Size = new Size(100, 100),
                Font = new FontSettings { FontFamily = EuropeFontAtlas.GetAtlas(), FontSize = 24 },
                Text = "HelloWorld",
                Location = new Point(0, 0)
            };

            Controls.Add(box);
        }

        private void Controls_ItemRemoved(GUIControl obj)
        {
            drawData.Remove(obj);
            obj.UIChanged -= Obj_UIChanged;
            OnUIChanged(this, drawData.Select(x => x.Value)
                .SelectMany(x => x)
                .ToList());
        }

        private void Controls_ItemAdded(GUIControl obj)
        {
            drawData.Add(obj, obj.Draw());
            obj.UIChanged += Obj_UIChanged;
            OnUIChanged(this, drawData.Select(x => x.Value)
                .SelectMany(x => x)
                .ToList());
        }

        private void Obj_UIChanged(GUIControl obj, List<IDrawData> data)
        {
            drawData[obj] = data;
            OnUIChanged(this, drawData.Select(x => x.Value)
                .SelectMany(x => x)
                .ToList());
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

        public override void OnSizeChanged(object sender, EventArgs args)
        {
            Size = ((MainForm)sender).Size;
            model.GameBasicCamera.Size = ((MainForm)sender).Size;
        }
    }
}