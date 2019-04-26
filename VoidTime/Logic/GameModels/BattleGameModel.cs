using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Timers;
using System.Windows.Forms;
using Box2DSharp.Dynamics;
using Timer = System.Timers.Timer;

namespace VoidTime
{
    public class BattleGameModel : BasicGameModel
    {
        private readonly Timer gameTick;
        private readonly object locker = new object();
        private readonly GameMap map;
        private readonly Player ship;
        private readonly BattleGameManager manager;

        private readonly BasicCamera GameBasicCamera;
        private bool Paused = true;

        public override World Physics { get; set; }
        public override Controls Controls { get; set; }
        public override TimeData Time { get; set; }

        public BattleGameModel(BattleGameModelData data)
        {
            ship = data.Player;
            ship.DeletePhysics();
            ship.Position = new Vector2D(data.MapSize.Width / 2, data.MapSize.Height / 2);

            GameBasicCamera = new SmoothCamera(data.CameraSize, ship);

            Controls = data.Controls;
            Controls.MouseHandler.ChangeCamera(GameBasicCamera);


            Time = data.Time;

            gameTick = new Timer(8);

            Physics = new World(new Vector2(0, 0));
            Physics.SetContactListener(new GlobalContactListner());

            map = new GameMap(new Size(1, 1), data.MapSize, Physics);

            map.AddGameObjects(ship);

            manager = new BattleGameManager(data.Enemy, map, ship);

            ship.OnDestroy += GameOver;

            gameTick.Elapsed += FrameTick;
        }

        public override event Action<List<GameObject>, BasicCamera> Tick;
        public override event Action<IGameModel> GameModelChanged;

        private void GameOver(GameObject obj)
        {
            Console.WriteLine("Lose");
        }

        public override void Run()
        {
            if (!Paused)
                return;

            Paused = false;
            gameTick.Start();
        }

        public override void Pause()
        {
            if (Paused)
                return;

            Paused = true;
            gameTick.Stop();
        }

        public override void OnSizeChanged(object sender, EventArgs args)
        {
            GameBasicCamera.Size = ((Form)sender).Size;
        }

        private void FrameTick(object sender, ElapsedEventArgs e)
        {
            lock (locker)
            {
                Time.Update();
                var activeObjects = map.GetGameObjects(GameBasicCamera);
                activeObjects.ForEach(x => x.Update());
                Physics.StepWithDelete(0.01666667F, 3, 6);
                map.UpdateMap(GameBasicCamera, GameBasicCamera.Size);
                GameBasicCamera.Update();
                Tick?.Invoke(activeObjects, GameBasicCamera);
                Controls.ClearOneFrameValues();
            }
        }
    }
}