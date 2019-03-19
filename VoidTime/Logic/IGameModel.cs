using System;
using System.Collections.Generic;

namespace VoidTime
{
    public interface IGameModel
    {
        void Run();
        void Pause();

        event Action<List<GameObject>, BasicCamera> Tick;
        event Action<IGameModel> GameModelChanged;
    }
}