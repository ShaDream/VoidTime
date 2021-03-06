﻿namespace VoidTime
{
    public class GunData : IItem
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public int Slot { get; set; }

        public string GetInfo()
        {
            return $"Slot {Slot}\n" +
                   $"{Name}\n" +
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
        public float Speed;

        public float RecoveryTime => 1000 / FireRate;
        public float currentRecovery;

        public override string ToString()
        {
            return Name;
        }
    }
}