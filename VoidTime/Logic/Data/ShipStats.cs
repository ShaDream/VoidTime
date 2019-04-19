using System;
using System.Collections.Generic;

namespace VoidTime
{
    public struct ShipStats
    {
        private float currentHP;
        public float MaxHP;
        public float Defence;
        public float Speed;

        public GunData[] Guns;



        public Blast[] Shoot(Player player)
        {
            var blastList = new List<Blast>();


            return blastList.ToArray();
        }

        private void UpdateGuns(Player player)
        {

        }

        public void UpdateStats(Player player)
        {
            UpdateGuns(player);
        }

        public void GetDamage(float damage)
        {
            damage *= Defence / 100;
            currentHP -= damage;
            if (currentHP <= 0)
                Death?.Invoke();
        }

        public void Heal(float heal)
        {
            currentHP = Math.Min(currentHP + heal, MaxHP);
        }

        public void SetHP(float hp)
        {
            currentHP = Math.Min(MaxHP, Math.Max(0, hp));
            if (currentHP <= 0)
                Death?.Invoke();
        }

        private event Action Death;
    }
}