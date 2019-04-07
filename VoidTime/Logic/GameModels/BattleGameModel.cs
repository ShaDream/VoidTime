using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class BattleGameModel : IGameModel
    {
        public World Physics { get; set; }
        public Controls Controls { get; set; }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public event Action<List<GameObject>, BasicCamera> Tick;
        public event Action<IGameModel> GameModelChanged;

        public void OnKeyPress(object sender, KeyEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnKeyRelease(object sender, KeyEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnMouseWheel(object sender, MouseEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnMouseMove(object sender, MouseEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnMouseClick(object sender, MouseEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void OnMouseDoubleClick(object sender, MouseEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}