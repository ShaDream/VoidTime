using System;
using System.Drawing;
using System.Windows.Forms;

namespace VoidTime.GUI
{
    public class BasicGameWindow : ISwitcheble
    {
        protected MainForm form;
        protected Window owner;
        public bool isShow { get; protected set; }

        protected Control window;

        public Point Location
        {
            get => window.Location;
            set => window.Location = value;
        }

        public Size Size
        {
            get => window.Size;
            set => window.Size = value;
        }

        protected virtual void Show()
        {
            form.BeginInvoke(new Action(() =>
            {
                Update();
                form.Controls.Add(window);
                window.BringToFront();
                form.currentModel.Pause();
            }));
        }

        protected virtual void Hide()
        {
            form.BeginInvoke(new Action(() =>
            {
                form.Controls.Remove(window);
                form.currentModel.Run();
            }));
            owner.lastKey = Keys.None;
        }

        public virtual void Switch()
        {
            if (isShow)
                Hide();
            else
                Show();
            isShow = !isShow;
        }

        protected virtual void Update() { }
    }
}