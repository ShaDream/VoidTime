using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class MainGameUI : UIWindow
    {
        private readonly MainGameModel model;

        public MainGameUI(MainGameModel model)
        {
            this.model = model;
            Controls.ItemAdded += Controls_ItemAdded;
            Controls.ItemRemoved += Controls_ItemRemoved;
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

        private void Obj_UIChanged(object obj, List<IDrawData> data)
        {
            drawData[(GUIControl) obj] = data;
            OnUIChanged(this, drawData.Select(x => x.Value)
                .SelectMany(x => x)
                .ToList());
        }

        public override void Unsubscribe()
        {
            throw new NotImplementedException();
        }

        public override void OnKeyPress(object sender, KeyEventArgs args)
        {
            model.OnKeyPress(sender, args);
        }

        public override void OnKeyRelease(object sender, KeyEventArgs args)
        {
            model.OnKeyRelease(sender, args);
        }

        public override void OnGameSizeChanged(object sender, EventArgs args)
        {
            Size = ((MainForm) sender).Size;
            model.GameBasicCamera.Size = ((MainForm) sender).Size;
        }
    }
}