using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Timers;
using System.Windows.Forms;
using Box2DSharp.Dynamics;
using VoidTime.Physics;
using Timer = System.Timers.Timer;

namespace VoidTime
{
    public class MainGameModel : IGameModel
    {
        private readonly Timer gameTick;
        private readonly PressedKeys keys = new PressedKeys();
        private readonly GameMap map;
        private readonly GameObject player;


        public BasicCamera GameBasicCamera;
        public bool Paused = true;


        public MainGameModel()
        {
            var axes = new HashSet<Axis>
            {
                new Axis("horizontal", Keys.D, Keys.A),
                new Axis("vertical", Keys.W, Keys.S)
            };
            var k = new ReadonlyKeys(keys, axes);

            gameTick = new Timer(16.66667F);

            Physics = new World(new Vector2(0, 0));
            Physics.SetContactListener(new GlobalContactListner());

            map = new GameMap(new Size(100, 100), new Size(1000, 1000), Physics);

            var planet = new Planet {Position = new Vector2D(5000, 5000), DrawingPriority = 1};
            const int border = 1000;
            var allowedCoordinates = new Rectangle(border,
                border,
                map.MapSize.Width - 2 * border,
                map.MapSize.Height - 2 * border);
            player = new Player(allowedCoordinates, new Vector2D(5000, 5000));
            var player2 = new Player(allowedCoordinates, new Vector2D(5000, 5010), false);

            map.AddGameObjects(new[] {planet, player, player2});
            GameBasicCamera = new SmoothCamera(new Size(), player);
            gameTick.Elapsed += FrameTick;
        }


        public event Action<List<GameObject>, BasicCamera> Tick;
        public event Action<IGameModel> GameModelChanged;
        public World Physics { get; set; }


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
            if (!keys.keys.Contains(args.KeyCode))
                keys.keys.Add(args.KeyCode);
        }

        public void OnKeyRelease(object sender, KeyEventArgs args)
        {
            if (keys.keys.Contains(args.KeyCode))
                keys.keys.Remove(args.KeyCode);
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


        private void FrameTick(object sender, ElapsedEventArgs e)
        {
            var activeObjects = map.GetGameObjects(GameBasicCamera);
            activeObjects.ForEach(x => x.Update());
            Physics.Step(0.01666667F, 3, 6);

            map.UpdateMap(GameBasicCamera);
            GameBasicCamera.Update();
            Tick?.Invoke(activeObjects, GameBasicCamera);
        }
    }
}