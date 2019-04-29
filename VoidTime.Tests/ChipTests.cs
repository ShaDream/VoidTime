using NUnit.Framework;
using NUnit.Framework.Internal;

namespace VoidTime.Tests
{
    [TestFixture]
    public class ChipTests
    {
        [Test]
        public void AddChip()
        {
            var ship = Init(100);
            ship.Chips.Add(ChipParser.GetChip("System Chip"));
        }

        [Test]
        public void RemoveChip()
        {
            var ship = Init(100);
            var chip = ChipParser.GetChip("System Chip");
            ship.Chips.Add(chip);
            ship.Chips.Remove(chip);
        }

        [Test]
        public void AddAllChips()
        {
            var ship = Init(100);
            var chips = ChipParser.GetAllChips();
            foreach (var chip in chips)
                ship.Chips.Add(chip);
        }

        [Test]
        public void CantAddChip()
        {
            var ship = Init(0);
            var chip = ChipParser.GetChip("System Chip");
            ship.Chips.Add(chip);
            Assert.AreEqual(0, ship.Chips.GetChips.Count);
        }

        [TestCase(0,false)]
        [TestCase(2,true)]
        [TestCase(20,true)]
        public void CheckCanAdd(int cost, bool expected)
        {
            var ship = Init(cost);
            var chip = ChipParser.GetChip("System Chip");
            Assert.AreEqual(expected, ship.Chips.IsCanAdd(chip));
        }

        private Ship Init(int chipCost)
        {
            var ship = new Ship();
            ship.Inventory = new Inventory(ship);
            ship.Data = new ShipData(ship);
            ship.Chips = new InstalledChips(ship, chipCost);
            return ship;
        }
    }
}
