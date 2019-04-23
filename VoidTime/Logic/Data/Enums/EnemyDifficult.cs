using System.ComponentModel;

namespace VoidTime
{
    public enum EnemyDifficult
    {
        [Description("Very easy")] VeryEasy,
        Easy,
        Normal,
        Hard,
        [Description("Very hard")] VeryHard,
        Extreame,
        Insane
    }
}