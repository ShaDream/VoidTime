using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VoidTime
{
    public struct ShipStats
    {
        private float currentHP;
        public float CurrentHP
        {
            get => currentHP;
            private set
            {
                currentHP = value;
                OnDamage?.Invoke();
            }
        }

        private float maxHP;
        public float MaxHP
        {
            get => maxHP;
            set
            {
                maxHP = value;
                OnChangeMaxHP?.Invoke();
            }
        }
        public float Defence;
        public float Speed;

        public GunData[] Guns;

        /// <param name="enemiesType">Allows only ShipTypes</param>
        public Blast[] Shoot(Ship ship, params Type[] enemiesType)
        {
            var blastList = new List<Blast>();

            for (var i = 0; i < Guns.Length; i++)
            {
                if (!(Math.Abs(Guns[i].currentRecovery) < float.Epsilon))
                    continue;

                Guns[i].currentRecovery = Guns[i].RecoveryTime;
                var blast = new Blast(ship.Position + Guns[i].PositionOffset.Rotate(ship.Angle),
                                      ship.Angle,
                                      Guns[i].Damage + (Random.IsLucky(Guns[i].CriticalChance) ? Guns[i].Damage : 0),
                                      ship,
                                      Guns[i].Range,
                                      Guns[i].Speed,
                                      enemiesType);
                blastList.Add(blast);
            }

            return blastList.ToArray();
        }

        private void UpdateGuns(Ship ship)
        {
            for (var index = 0; index < Guns.Length; index++)
                Guns[index].currentRecovery = Math.Max(0, Guns[index].currentRecovery - Time.DeltaTime);
        }

        public void UpdateStats(Ship ship)
        {
            UpdateGuns(ship);
        }

        public void GetDamage(float damage)
        {
            damage *= 1 - Defence / 100;
            CurrentHP -= damage;
            if (CurrentHP <= 0)
                Death?.Invoke();
        }

        public void Heal(float heal)
        {
            CurrentHP = Math.Min(CurrentHP + heal, MaxHP);
        }

        public void SetHP(float hp)
        {
            CurrentHP = Math.Min(MaxHP, Math.Max(0, hp));
            if (CurrentHP <= 0)
                Death?.Invoke();
        }

        public string GetInfo()
        {
            var gunInfo = new StringBuilder();
            foreach (var gun in Guns)
            {
                gunInfo.Append(gun.GetInfo());
                gunInfo.Append("\n");
            }

            return $"Health: {CurrentHP}\n" +
                   $"Defence: {Defence}\n" +
                   $"Speed: {Speed}\n\n" +
                   gunInfo;
        }

        public event Action Death;
        public event Action OnDamage;
        public event Action OnChangeMaxHP;
    }
}