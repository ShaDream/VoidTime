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
        public float AutoHeal;

        public bool DisplayHP;
        public bool Destroy;
        public bool CanMove;
    }
}