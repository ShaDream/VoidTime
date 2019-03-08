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
        private Timer gameTick;
        private GameMap map;
        public Camera GameCamera;
        public bool Paused = true;
        private GameObject player;
        private PressedKeys keys = new PressedKeys();

        public GameModel()
        {
            ReadonlyKeys k = new ReadonlyKeys(keys, new HashSet<Axis>());

            gameTick = new Timer(16);
            player = new Player("player", new Vector2D(10000, 10000));
            map = new GameMap(new Size(100, 100), new Size(1000, 1000), new[] { player });
            GameCamera = new Camera(player, new Size(800, 800));
            gameTick.Elapsed += FrameTick;
        }

        private void FrameTick(object sender, ElapsedEventArgs e)
        {
            var activeObjects = map.GetGameObjects(GameCamera);
            activeObjects.ForEach(x => x.Update());
            GameCamera.Update();
            var s = activeObjects.Select(x => GameCamera.GamePositionToWindow(x.Position)).ToList();
            Tick?.Invoke(activeObjects, s);
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
            if (!keys.keys.Contains(args.KeyCode))
                keys.keys.Add(args.KeyCode);
        }

        public void OnKeyRelease(object sender, KeyEventArgs args)
        {
            if (keys.keys.Contains(args.KeyCode))
                keys.keys.Remove(args.KeyCode);
        }

        public event Action<List<GameObject>, List<Vector2D>> Tick;
    }
}