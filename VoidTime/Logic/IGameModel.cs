using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VoidTime
{
    public interface IGameModel
    {
        void Run();
        void Pause();

        event Action<List<GameObject>, BasicCamera> Tick;
        event Action<IGameModel> GameModelChanged;

        void OnKeyPress(object sender, KeyEventArgs args);
        void OnKeyRelease(object sender, KeyEventArgs args);
        void OnMouseWheel(object sender, MouseEventArgs args);
        void OnMouseMove(object sender, MouseEventArgs args);
        void OnMouseClick(object sender, MouseEventArgs args);
        void OnMouseDoubleClick(object sender, MouseEventArgs args);
    }
}