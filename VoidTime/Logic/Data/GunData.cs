namespace VoidTime
{
    public struct GunData : IItem
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public string GetInfo()
        {
            return $"{Name}:\n" +
                   $"Damage: {Damage}\n" +
                   $"Tier: {Tier}\n" +
                   $"Fire rate: {FireRate}\n" +
                   $"Critical chance: {CriticalChance}\n" +
                   $"Range: {Range}\n" +
                   $"Price: {Price}\n";
        }

        public Vector2D PositionOffset;
        public float Range;
        public float Damage;
        public int Tier;
        public float FireRate;
        private float recovery;
        private Vector2D direction;
        public bool CanRotate;
        public float CriticalChance;

        public float RecoveryTime => 1000 / FireRate;
        public float currentRecovery;

        public override string ToString()
        {
            return Name;
        }
    }
}