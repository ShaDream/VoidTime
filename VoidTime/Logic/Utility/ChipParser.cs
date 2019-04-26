using System;
using System.Collections.Generic;
using System.Xml;
using VoidTime.Resources;

namespace VoidTime
{
    public static class ChipParser
    {
        private static readonly Dictionary<int, Type> chipsType = new Dictionary<int, Type>
        {
            {0, typeof(HPGaugeChip)},
            {1, typeof(SystemChip)},
            {2, typeof(ControlChip)},
            {3, typeof(AttackUpChip)},
            {4, typeof(RangUpChip)},
            {5, typeof(CriticalUpChip)},
            {6, typeof(AutoHealChip)},
            {7, typeof(MaxHPUpChip)},
            {8, typeof(DefenceUpChip)},
            {9, typeof(DropRateUpChip)},
            {10, typeof(MovingSpeedUpChip)}
        };

        private static Dictionary<string, Chip> chips = new Dictionary<string, Chip>();

        private static readonly bool Initialized = false;

        public static Chip GetChip(string name, int level = 1)
        {
            if (!Initialized)
                Init();

            var chip = chips[name].Copy();
            chip.CurrentLevel = level;
            return chip;
        }

        public static Chip[] GetAllChips()
        {
            if (!Initialized)
                Init();

            var resultChips = new Chip[chips.Count];
            var i = 0;
            foreach (var chip in chips)
            {
                var c = chip.Value.Copy();
                resultChips[i] = c;
                i++;
            }

            return resultChips;
        }

        private static void Init()
        {
            var doc = new XmlDocument();
            doc.LoadXml(Data.Chips);
            var chipNodes = doc.SelectNodes("//chip");
            foreach (XmlElement chipNode in chipNodes)
                chips.Add(chipNode["name"].InnerText, ParseChip(chipNode["name"].InnerText));
        }

        private static Chip ParseChip(string name)
        {
            var doc = new XmlDocument();
            doc.LoadXml(Data.Chips);
            var node = doc.SelectSingleNode($"//chip[name=\'{name}\']");

            var id = int.Parse(node["id"].InnerText);
            var chip = (Chip)Activator.CreateInstance(chipsType[id]);

            chip.Type = (ChipType)Enum.Parse(typeof(ChipType), node["class"].InnerText, true);

            var costsNodes = doc.SelectNodes($"//chip[name=\'{name}\']//cost");
            chip.Costs = new int[costsNodes.Count];
            foreach (XmlNode costsNode in costsNodes)
            {
                var lvl = int.Parse(costsNode.Attributes["level"].Value) - 1;
                chip.Costs[lvl] = int.Parse(costsNode.InnerText);
            }

            chip.CurrentLevel = 1;

            var valueNodes = doc.SelectNodes($"//chip[name=\'{name}\']/value");
            chip.Values = new float[valueNodes.Count];
            foreach (XmlNode valueNode in costsNodes)
            {
                var lvl = int.Parse(valueNode.Attributes["level"].Value) - 1;
                chip.Values[lvl] = int.Parse(valueNode.InnerText);
            }

            chip.Name = name;

            chip.Description = node["description"].InnerText;
            chip.MaxLevel = int.Parse(node["levels"].InnerText);

            return chip;
        }
    }
}