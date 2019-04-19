﻿namespace VoidTime
{
    public struct GunData : IItem
    {
        public string Name { get; set; }
        public Vector2D PositionOffset;
        public float Range;
        public float Damage;
        public int Tier;
        public float FireRate;
        private float recovery;
        private Vector2D direction;
        public bool CanRotate;
        public float CriticalChance;
        public int Price;

        public float RecoveryTime => 1000 / FireRate;
        public float currentRecovery;
    }
}