using System;

namespace VoidTime.Manager
{
    public class BattleGameManager
    {
        private static readonly int[] aliveEnemiesCount = {3, 4, 5, 6, 7, 8, 6};

        private EnemyDifficult difficulty;
        private int enemies;
        private readonly int enemiesToCreate;

        private GameMap map;

        private int alivesEnemies
        {
            get => enemies;
            set
            {
                enemies = value;
                if (enemies < aliveEnemiesCount[(int) difficulty] && enemiesToCreate > 0)
                    CreateEnemy();
            }
        }

        public BattleGameManager(MapEnemy enemy, GameMap map)
        {
            this.map = map;
            enemiesToCreate = enemy.EnemiesCount;
        }

        public void CreateEnemy()
        {
            var enemy = new BattleEnemy();
            alivesEnemies++;
            enemy.OnDestroy += o =>
            {
                alivesEnemies--;
                if (alivesEnemies == 0 && enemiesToCreate == 0)
                    Win?.Invoke();
            };
        }

        public event Action Win;
    }
}