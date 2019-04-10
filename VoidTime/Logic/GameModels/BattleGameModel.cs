using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Timers;
using System.Windows.Forms;
using Box2DSharp.Dynamics;
using Timer = System.Timers.Timer;

namespace VoidTime
{
    public class BattleGameModel : IGameModel
    {
        private readonly object locker = new object();

        public World Physics { get; set; }
        public Controls Controls { get; set; } = new Controls();
        private readonly Timer gameTick;
        private readonly GameMap map;
        private readonly BattleShipPlayer player;

        public BasicCamera GameBasicCamera;
        public bool Paused = true;

        public int EnemyCreateRecovery = 1000;
        public int currentCreateRecovery = 0;

        public event Action<List<GameObject>, BasicCamera> Tick;
        public event Action<IGameModel> GameModelChanged;
        public event Action<BattleEndData> BattleIsOver;


        public BattleGameModel(BattleGameModelData data)
        {
            var axes = new HashSet<Axis>
            {
                new Axis("horizontal", Keys.D, Keys.A),
                new Axis("vertical", Keys.W, Keys.S)
            };
            Controls.AxesHandler = axes.ToDictionary(x => x.Name);
            var k = new ReadonlyKeys(Controls);

            gameTick = new Timer(16.66667F);

            Physics = new World(new Vector2(0, 0));
            Physics.SetContactListener(new GlobalContactListner());

            map = new GameMap(new Size(1, 1), data.MapSize, Physics);
            player = new BattleShipPlayer(new Vector2D(data.MapSize.Width / 2, data.MapSize.Width / 2), 100, 100);

            map.AddGameObjects(player);
            player.OnDestroy += GameOver;

            GameBasicCamera = new SmoothCamera(new Size(), player);
            gameTick.Elapsed += FrameTick;
        }

        private void GameOver(GameObject obj)
        {
        }

        public void Run()
        {
            if (!Paused)
                return;

            Paused = false;
            gameTick.Start();
        }

        public void Pause()
        {
            if (Paused)
                return;

            Paused = true;
            gameTick.Stop();
        }

        public void OnKeyPress(object sender, KeyEventArgs args)
        {
            if (!Controls.KeysHandler.Contains(args.KeyCode))
                Controls.KeysHandler.Add(args.KeyCode);
        }

        public void OnKeyRelease(object sender, KeyEventArgs args)
        {
            if (Controls.KeysHandler.Contains(args.KeyCode))
                Controls.KeysHandler.Remove(args.KeyCode);
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

        public void OnSizeChanged(object sender, EventArgs args)
        {
            GameBasicCamera.Size = ((Form)sender).Size;
        }

        private void FrameTick(object sender, ElapsedEventArgs e)
        {
            lock (locker)
            {
                var activeObjects = map.GetGameObjects(GameBasicCamera);
                activeObjects.ForEach(x => x.Update());
                Physics.Step(0.01666667F, 3, 6);
                map.UpdateMap(GameBasicCamera, GameBasicCamera.Size);
                GameBasicCamera.Update();
                Tick?.Invoke(activeObjects, GameBasicCamera);
                if (currentCreateRecovery <= 0)
                {
                    map.AddGameObject(new BattleShipEnemy(new Vector2D(map.MapSize.Width / 2+1, map.MapSize.Width / 2),
                        player, 100, 100));
                    currentCreateRecovery = EnemyCreateRecovery;
                }
                currentCreateRecovery--;
            }
        }
    }
}