using System;

namespace VoidTime.GUI
{
    public static class GUIFactory
    {
        public static UIWindow Create(IGameModel model)
        {
            switch (model)
            {
                case MainGameModel _:
                    var MainGameUI = new MainGameUI(model as MainGameModel);
                    return MainGameUI;
            }

            throw new NotImplementedException();
        }
    }
}