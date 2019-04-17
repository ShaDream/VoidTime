namespace VoidTime
{
    public struct PlayerStats
    {
        public string ShipName;
        public string[] Guns;

        public BattleShipStatsData HP;
        public float MovementSpeed;
        public float CriticalChance;
        public float DropRateChance;

        public float MovementSpeedIncrease;
        public float CriticalChanceIncrease;
        public float DropRateChanceIncrease;
        public float AutoHeal;
        public float AttackUp;
        public float DefenceUp;
        public float RangUp;
        public float HPUp;

        public bool DisplayHP;
        public bool Destroy;
        public bool CanMove;
    }
}