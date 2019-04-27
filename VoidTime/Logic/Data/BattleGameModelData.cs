using System.Drawing;

namespace VoidTime
{
    public struct BattleGameModelData
    {
        public Size MapSize;
        public Player Player;
        public MapEnemy Enemy;
        public Controls Controls;
        public TimeData Time;
        public Size CameraSize;
        public IGameModel MainModel;
    }
}