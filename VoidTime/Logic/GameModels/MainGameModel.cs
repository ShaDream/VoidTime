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
        public readonly Player player;


        public BasicCamera GameBasicCamera;
        public bool Paused = true;

        public override Controls Controls { get; set; }
        public override World Physics { get; set; }
        public override TimeData Time { get; set; }

        public MainGameModel()
        {
            Physics = new World(new Vector2(0, 0));
            Physics.SetContactListener(new GlobalContactListner());

            map = new GameMap(new Size(100, 100), new Size(1000, 1000), Physics);

            const int border = 3000;
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
            Input.Create(Controls);

            Time = new TimeData();
            VoidTime.Time.Create(Time);

            gameTick = new Timer(16.66667F);

            var planet = new Planet {Position = new Vector2D(5000, 5000), DrawingPriority = 1};

            map.AddGameObjects(planet, player);
            gameTick.Elapsed += FrameTick;

            player.StartingBattle += StartBattle;
        }

        private void StartBattle(MapEnemy obj)
        {
            Pause();
            var data = new BattleGameModelData();
            var model = new BattleGameModel(data);
            model.GameModelChanged += (m) =>
            {
                obj.Destoy();
                player.CreatePhysics(Physics);
                player.Position = m.;
                Run();
            };
            GameModelChanged?.Invoke(model);
            model.Run();
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
                Time.Update();
                Physics.StepWithDelete(0.01666667F, 3, 6);
                var activeObjects = map.GetGameObjects(GameBasicCamera, GameBasicCamera.Size);
                activeObjects.ForEach(x => x.Update());
                map.UpdateMap(GameBasicCamera, GameBasicCamera.Size);
                GameBasicCamera.Update();
                Tick?.Invoke(activeObjects, GameBasicCamera);
                Controls.ClearOneFrameValues();
            }
        }

        public void AddObjectOnMap(GameObject obj)
        {
            map.AddGameObject(obj);
        }

    }
}