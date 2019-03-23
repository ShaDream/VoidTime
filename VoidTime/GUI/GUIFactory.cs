using System;

namespace VoidTime.GUI
{
    public static class GUIFactory
    {
        public static UIWindow Create(IGameModel model)
        {
            switch (model)
            {
                case GameModel _:
                    var MainGameUI = new MainGameUI(model as GameModel);
                    return MainGameUI;
            }

            throw new NotImplementedException();
        }
    }
}