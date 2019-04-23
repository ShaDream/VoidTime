using System;

namespace VoidTime
{
    public class Quest
    {
        public Func<Ship> BeginQuest;
        public EnemyCount Count;
        public EnemyDifficult Difficult;
        public string Name;
        public int Reward;
        public QuestStatus Status;

        public Quest(string name,
                     int reward,
                     EnemyCount count,
                     EnemyDifficult difficult,
                     Func<Ship> beginQuest)
        {
            Name = name;
            Reward = reward;
            Count = count;
            Difficult = difficult;
            Status = QuestStatus.NotStarted;
            BeginQuest = beginQuest;
        }

        public void AcceptQuest()
        {
            BeginQuest.Invoke().OnDestroy += o => { Status = QuestStatus.Сompleted; };
            Status = QuestStatus.Performed;
        }

        public override string ToString()
        {
            return Name;
        }

        public string GetInfo()
        {
            return $"Reward: {Reward}\n" +
                   $"QuestStatus: {Status.ToString()}\n" +
                   $"Enemy Difficult: {Difficult.ToString()}\n" +
                   $"Enemy Count: {Count.ToString()}\n";
        }
    }
}