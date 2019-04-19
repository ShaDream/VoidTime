using System.Linq;

namespace VoidTime
{
    public struct ShipData
    {
        private ShipBaseData shipBase;

        public Buffs ShipBuffs;

        public ShipStats ShipStats;

        //TODO:Create inventory and chipList

        public void SetShip(string name)
        {
            shipBase = ShipParser.GetShip(name);
            UpdateFields();
        }

        public void SetGun(string name, int slot)
        {
            shipBase.slots[slot].Gun = GunParser.GetGun(name);
            shipBase.slots[slot].HasGun = true;
            UpdateFields();
        }

        public void RemoveGun(int slot)
        {
            shipBase.slots[slot].HasGun = false;
            UpdateFields();
        }

        public void UpdateFields()
        {
            ShipStats.MaxHP = shipBase.HP + shipBase.HP * ShipBuffs.HPUp;
            ShipStats.Defence = shipBase.Defence + shipBase.Defence * ShipBuffs.DefenceUp;
            ShipStats.Speed = shipBase.MoveSpeed + shipBase.MoveSpeed * ShipBuffs.MoveSpeedIncrease;
            ShipStats.Guns = new GunData[shipBase.slots.Count(x => x.HasGun)];

            var guns = shipBase.slots.Where(x => x.HasGun);
            var i = 0;
            foreach (var slot in guns)
            {
                ShipStats.Guns[i].PositionOffset = slot.positionOffset;
                var gun = slot.Gun;
                ShipStats.Guns[i].CanRotate = gun.CanRotate;
                ShipStats.Guns[i].CriticalChance =
                    gun.CriticalChance + gun.CriticalChance * ShipBuffs.CriticalChanceIncrease;
                ShipStats.Guns[i].Damage = gun.Damage + gun.Damage * ShipBuffs.AttackUp;
                ShipStats.Guns[i].FireRate = gun.FireRate;
                ShipStats.Guns[i].GunName = gun.GunName;
                ShipStats.Guns[i].Range = gun.Range + gun.Range * ShipBuffs.RangUp;
                i++;
            }
        }
    }
}