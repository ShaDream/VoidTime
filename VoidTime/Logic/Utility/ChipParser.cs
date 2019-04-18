﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace VoidTime
{
    public static class ChipParser
    {
        private static Dictionary<int, Type> chips = new Dictionary<int, Type>
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

        public static Chip GetChip(string name, int level = 1)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Resources.Data.Chips);
            var node = doc.SelectSingleNode($"//chip[name=\'{name}\']");

            Chip chip;
            var id = int.Parse(node["id"].InnerText);
            chip = (Chip)Activator.CreateInstance(chips[id]);

            chip.Type = (ChipType)Enum.Parse(typeof(ChipType), node["class"].InnerText, true);

            var costsNodes = doc.SelectNodes($"//chip[name=\'{name}\']//cost");
            chip.Costs = new int[costsNodes.Count];
            foreach (XmlNode costsNode in costsNodes)
            {
                var lvl = int.Parse(costsNode.Attributes["level"].Value)-1;
                chip.Costs[lvl] = int.Parse(costsNode.InnerText);
            }

            chip.CurrentLevel = level;

            var valueNodes = doc.SelectNodes($"//chip[name=\'{name}\']/value");
            chip.Values = new float[valueNodes.Count];
            foreach (XmlNode valueNode in costsNodes)
            {
                var lvl = int.Parse(valueNode.Attributes["level"].Value)-1;
                chip.Values[lvl] = int.Parse(valueNode.InnerText);
            }

            chip.Name = name;

            chip.Description = node["description"].InnerText;
            chip.MaxLevel = int.Parse(node["levels"].InnerText);

            return chip;
        }
    }
}