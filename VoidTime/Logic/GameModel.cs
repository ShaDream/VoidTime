using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace VoidTime
{
    public class GameModel
    {

        #region Private Fields

        private Timer gameTick;
        private GameMap map;
        private GameObject player;
        private PressedKeys keys = new PressedKeys();

        #endregion

        #region Public Properties

        public BasicCamera GameBasicCamera;
        public bool Paused = true;

        #endregion

        #region Constructor

        public GameModel()
        {
            var axes = new HashSet<Axis>();
            axes.Add(new Axis("horizontal", Keys.D, Keys.A));
            axes.Add(new Axis("vertical", Keys.W, Keys.S));
            ReadonlyKeys k = new ReadonlyKeys(keys, axes);

            gameTick = new Timer(16);
            player = new Player("player", new Vector2D(10000, 10000));
            map = new GameMap(new Size(100, 100), new Size(1000, 1000), new[] { player });
            GameBasicCamera = new SmoothCamera(new Size(800, 800), player);
            gameTick.Elapsed += FrameTick;
        }

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

        #endregion

        #region Public Events

        public event Action<List<GameObject>, BasicCamera> Tick;

        #endregion

    }
}