using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace VoidTime
{
    public class GameModel : IGameModel
    {
        #region Private Fields

        private readonly Timer gameTick;
        private readonly GameMap map;
        private readonly GameObject player;
        private readonly PressedKeys keys = new PressedKeys();

        #endregion

        #region Public Properties

        public BasicCamera GameBasicCamera;
        public bool Paused = true;

        #endregion

        #region Constructor

        public GameModel()
        {
            var axes = new HashSet<Axis>
            {
                new Axis("horizontal", Keys.D, Keys.A),
                new Axis("vertical", Keys.W, Keys.S)
            };
            var k = new ReadonlyKeys(keys, axes);

            gameTick = new Timer(16);

            map = new GameMap(new Size(100, 100), new Size(1000, 1000));

            var planet = new Planet { Position = new Vector2D(5000, 5000), DrawingPriority = 1 };
            const int border = 1000;
            var allowedCoordinates = new Rectangle(border,
                                                   border,
                                                   map.MapSize.Width - 2 * border,
                                                   map.MapSize.Height - 2 * border);
            player = new Player(allowedCoordinates, new Vector2D(5000, 5000));

            map.AddGameObjects(new[] { planet, player });
            GameBasicCamera = new SmoothCamera(new Size(), player);
            gameTick.Elapsed += FrameTick;
        }

        #endregion

        #region Public Events

        public event Action<List<GameObject>, BasicCamera> Tick;
        public event Action<IGameModel> GameModelChanged;

        #endregion

        #region Private Methods

        private void FrameTick(object sender, ElapsedEventArgs e)
        {
            var activeObjects = map.GetGameObjects(GameBasicCamera);
            activeObjects.ForEach(x => x.Update());
            map.UpdateMap(GameBasicCamera);
            GameBasicCamera.Update();
            Tick?.Invoke(activeObjects, GameBasicCamera);
        }

        #endregion

        #region Public Methods

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

        #endregion
    }
}