using System.Linq;

namespace VoidTime
{
    public struct ShipData
    {
        private readonly Ship owner;

        private ShipBaseData shipBase;

        public Buffs ShipBuffs;

        public ShipStats ShipStats;

        public ShipData(Ship owner)
        {
            this.owner = owner;
            shipBase = new ShipBaseData();
            ShipBuffs = new Buffs();
            ShipStats = new ShipStats();
        }

        public void SetShip(ShipBaseData data)
        {
            if (shipBase.slots != null)
                foreach (var shipBaseSlot in shipBase.slots)
                    if (shipBaseSlot.HasGun)
                        owner.Inventory.Add(shipBaseSlot.Gun);

            shipBase = data;
            UpdateFields();
        }

        public void SetGun(GunData data, int slot)
        {
            if (shipBase.slots[slot].MaxTier < data.Tier)
                return;
            if (shipBase.slots[slot].HasGun)
                owner.Inventory.Add(shipBase.slots[slot].Gun);
            data.Slot = slot;
            shipBase.slots[slot].Gun = data;
            shipBase.slots[slot].HasGun = true;
            UpdateFields();
        }

        public void RemoveGun(int slot)
        {
            if (shipBase.slots[slot].HasGun)
                owner.Inventory.Add(shipBase.slots[slot].Gun);
            shipBase.slots[slot].HasGun = false;
            UpdateFields();
        }

        public ShipSlotData[] GetSlots()
        {
            return shipBase.slots;
        }

        public void UpdateFields()
        {
            ShipStats.MaxHP = shipBase.HP + shipBase.HP * ShipBuffs.HPUp / 100;
            ShipStats.Defence = shipBase.Defence + shipBase.Defence * ShipBuffs.DefenceUp / 100;
            ShipStats.Speed = shipBase.MoveSpeed + shipBase.MoveSpeed * ShipBuffs.MoveSpeedIncrease / 100;
            ShipStats.Guns = new GunData[shipBase.slots.Count(x => x.HasGun)];

            var guns = shipBase.slots.Where(x => x.HasGun);
            var i = 0;
            foreach (var slot in guns)
            {
                ShipStats.Guns[i].PositionOffset = slot.positionOffset;
                var gun = slot.Gun;
                ShipStats.Guns[i].Slot = gun.Slot;
                ShipStats.Guns[i].CanRotate = gun.CanRotate;
                ShipStats.Guns[i].CriticalChance =
                    gun.CriticalChance + gun.CriticalChance * ShipBuffs.CriticalChanceIncrease / 100;
                ShipStats.Guns[i].Damage = gun.Damage + gun.Damage * ShipBuffs.AttackUp / 100;
                ShipStats.Guns[i].FireRate = gun.FireRate;
                ShipStats.Guns[i].Name = gun.Name;
                ShipStats.Guns[i].Range = gun.Range + gun.Range * ShipBuffs.RangUp / 100;
                i++;
            }
        }
    }
}