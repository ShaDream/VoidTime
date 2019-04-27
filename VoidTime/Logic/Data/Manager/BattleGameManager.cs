using System;

namespace VoidTime
{
    public class BattleGameManager
    {
        private static readonly int[] aliveEnemiesCount = { 3, 4, 5, 6, 7, 8, 6 };

        private EnemyDifficult difficulty;
        private int aliveEnemies;
        private readonly int enemiesToCreate;
        private int leftToCreate;

        private GameMap map;
        private Player player;


        public BattleGameManager(MapEnemy enemy, GameMap map, Player player)
        {
            difficulty = enemy.Difficult;
            this.map = map;
            enemiesToCreate = leftToCreate = enemy.EnemiesCount;
            this.player = player;
            aliveEnemies = 0;
        }

        private void CreateEnemy()
        {
            var enemy = EnemyCreator.CreateEnemy(difficulty, (EnemyBehaviorType.CloseCombat));
            enemy.Position = new Vector2D(Random.Next(map.MapSize.Width), Random.Next(map.MapSize.Height));
            enemy.player = player;
            aliveEnemies++;
            leftToCreate--;
            enemy.OnDestroy += o =>
            {
                aliveEnemies--;
                if (aliveEnemies == 0 && leftToCreate == 0)
                    Win?.Invoke();
            };
            map.AddGameObject(enemy);
        }

        public void Update()
        {
            var needToCreate = Math.Min(aliveEnemiesCount[(int) difficulty] - aliveEnemies,leftToCreate);
            for (int i = 0; i < needToCreate; i++)
                CreateEnemy();
        }

        public event Action Win;
    }
}