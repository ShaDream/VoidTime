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
    public class MainGameModel : BasicGameModel
    {
        private readonly Timer gameTick;
        private readonly GameMap map;
        private readonly GameObject player;


        public BasicCamera GameBasicCamera;
        public bool Paused = true;
        public override Controls Controls { get; set; }
        public override World Physics { get; set; }

        public MainGameModel()
        {
            const int border = 1000;
            var allowedCoordinates = new Rectangle(border,
                border,
                map.MapSize.Width - 2 * border,
                map.MapSize.Height - 2 * border);

            player = new Player(allowedCoordinates, new Vector2D(5000, 5000));
            GameBasicCamera = new SmoothCamera(new Size(), player);

            Controls = new Controls(GameBasicCamera);

            var axes = new HashSet<Axis>
            {
                new Axis("horizontal", Keys.D, Keys.A),
                new Axis("vertical", Keys.W, Keys.S)
            };
            Controls.AxesHandler = axes.ToDictionary(x => x.Name);
            var k = new Input(Controls);

            gameTick = new Timer(16.66667F);

            Physics = new World(new Vector2(0, 0));
            Physics.SetContactListener(new GlobalContactListner());

            map = new GameMap(new Size(100, 100), new Size(1000, 1000), Physics);

            var planet = new Planet {Position = new Vector2D(5000, 5000), DrawingPriority = 1};

            var player2 = new Player(allowedCoordinates, new Vector2D(5000, 5010), false);

            map.AddGameObjects(planet, player, player2);
            gameTick.Elapsed += FrameTick;
        }

        public override event Action<List<GameObject>, BasicCamera> Tick;
        public override event Action<IGameModel> GameModelChanged;

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
            GameBasicCamera.Size = ((Form) sender).Size;
        }

        private void FrameTick(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                var activeObjects = map.GetGameObjects(GameBasicCamera, GameBasicCamera.Size);
                activeObjects.ForEach(x => x.Update());
                Physics.StepWithDelete(0.01666667F, 3, 6);
                map.UpdateMap(GameBasicCamera, GameBasicCamera.Size);
                GameBasicCamera.Update();
                Tick?.Invoke(activeObjects, GameBasicCamera);
            }
        }
    }
}