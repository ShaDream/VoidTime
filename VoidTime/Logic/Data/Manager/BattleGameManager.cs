using System;

namespace VoidTime
{
    public class BattleGameManager
    {
        private static readonly int[] aliveEnemiesCount = { 3, 4, 5, 6, 7, 8, 6 };

        private EnemyDifficult difficulty;
        private int _alivesEnemies;
        private readonly int enemiesToCreate;

        private GameMap map;
        private Player player;

        private int alivesEnemies
        {
            get => _alivesEnemies;
            set
            {
                _alivesEnemies = value;
                if (_alivesEnemies < aliveEnemiesCount[(int)difficulty] && enemiesToCreate > 0)
                    CreateEnemy();
            }
        }

        public BattleGameManager(MapEnemy enemy, GameMap map, Player player)
        {
            difficulty = enemy.Difficult;
            this.map = map;
            enemiesToCreate = enemy.EnemiesCount;
            this.player = player;
            alivesEnemies = 0;
        }

        private void CreateEnemy()
        {
            var enemy = EnemyCreator.CreateEnemy(difficulty, (EnemyBehaviorType.CloseCombat));
            enemy.Position = new Vector2D(Random.Next(map.MapSize.Width), Random.Next(map.MapSize.Height));
            enemy.player = player;
            alivesEnemies++;
            enemy.OnDestroy += o =>
            {
                alivesEnemies--;
                if (alivesEnemies == 0 && enemiesToCreate == 0)
                    Win?.Invoke();
            };
            map.AddGameObject(enemy);
        }

        public event Action Win;
    }
}