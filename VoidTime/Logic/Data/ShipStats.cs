using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VoidTime
{
    public struct ShipStats
    {
        private float currentHP;
        public float MaxHP;
        public float Defence;
        public float Speed;

        public GunData[] Guns;


        public Blast[] Shoot(Ship ship)
        {
            var blastList = new List<Blast>();

            if (!Input.GetMouseButton(MouseButtons.Left))
                return blastList.ToArray();

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
                                      typeof(Ship));
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

        public string GetInfo()
        {
            var gunInfo = new StringBuilder();
            foreach (var gun in Guns)
            {
                gunInfo.Append(gun.GetInfo());
                gunInfo.Append("\n");
            }

            return $"Health: {currentHP}\n" +
                   $"Defence: {Defence}\n" +
                   $"Speed: {Speed}\n\n" +
                   gunInfo;
        }

        private event Action Death;
    }
}