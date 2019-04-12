﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Timers;
using System.Windows.Forms;
using Box2DSharp.Collision;
using Box2DSharp.Collision.Collider;
using Box2DSharp.Dynamics;
using Timer = System.Timers.Timer;

namespace VoidTime
{
    public class BattleGameModel : BasicGameModel
    {
        private readonly Timer gameTick;
        private readonly object locker = new object();
        private readonly GameMap map;
        private readonly BattleShipPlayer player;
        public int currentCreateRecovery;

        public int EnemyCreateRecovery = 5;

        public BasicCamera GameBasicCamera;
        public bool Paused = true;

        public override World Physics { get; set; }
        public override Controls Controls { get; set; }
        public override TimeData Time { get; set; }
        public float average;


        public BattleGameModel(BattleGameModelData data)
        {
            player = new BattleShipPlayer(new Vector2D(data.MapSize.Width / 2, data.MapSize.Width / 2), 1000000, 1000000);
            GameBasicCamera = new SmoothCamera(new Size(), player);

            Controls = new Controls(GameBasicCamera);

            var axes = new HashSet<Axis>
            {
                new Axis("horizontal", Keys.D, Keys.A),
                new Axis("vertical", Keys.W, Keys.S)
            };
            Controls.AxesHandler = axes.ToDictionary(x => x.Name);
            Time = new TimeData();

            Input.Create(Controls);
            VoidTime.Time.Create(Time);

            gameTick = new Timer(8);

            Physics = new World(new Vector2(0, 0));
            Physics.SetContactListener(new GlobalContactListner());

            map = new GameMap(new Size(1, 1), data.MapSize, Physics);

            map.AddGameObjects(player);
            player.OnDestroy += GameOver;

            gameTick.Elapsed += FrameTick;
        }

        public override event Action<List<GameObject>, BasicCamera> Tick;
        public override event Action<IGameModel> GameModelChanged;
        public event Action<BattleEndData> BattleIsOver;

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
            GameBasicCamera.Size = ((Form) sender).Size;
        }

        private void FrameTick(object sender, ElapsedEventArgs e)
        {
            lock (locker)
            {
                Time.Update();
                CreateEnemy();
                var activeObjects = map.GetGameObjects(GameBasicCamera);
                activeObjects.ForEach(x => x.Update());
                Physics.StepWithDelete(0.01666667F, 3, 6);
                map.UpdateMap(GameBasicCamera, GameBasicCamera.Size);
                GameBasicCamera.Update();
                Tick?.Invoke(activeObjects, GameBasicCamera);
                Controls.ClearOneFrameValues();
            }
        }

        private void CreateEnemy()
        {
            if (currentCreateRecovery <= 0)
            {
                map.AddGameObject(new BattleShipEnemy(new Vector2D(map.MapSize.Width / 2 + 1000, map.MapSize.Width / 2),
                    player, 100, 100));
                currentCreateRecovery = EnemyCreateRecovery;
            }

            currentCreateRecovery--;
        }
    }
}