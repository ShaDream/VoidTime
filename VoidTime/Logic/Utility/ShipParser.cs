using System.Collections.Generic;
using System.Xml;
using VoidTime.Resources;

namespace VoidTime
{
    public static class ShipParser
    {
        //TODO:Check for exception
        public static ShipBaseData GetShip(string name)
        {
            var doc = new XmlDocument();
            doc.LoadXml(Data.Ships);
            var node = doc.SelectSingleNode($"//ship[name=\'{name}\']");

            var ship = new ShipBaseData
            {
                Name = name,
                HP = float.Parse(node["hp"].InnerText),
                Defence = float.Parse(node["defence"].InnerText),
                MoveSpeed = float.Parse(node["speed"].InnerText),
                slots = new ShipSlotData[node["slots"].ChildNodes.Count]
            };

            var slotNodes = doc.SelectNodes($"//ship[name=\'{name}\']//slots//slot");

            for (var i = 0; i < slotNodes.Count; i++)
            {
                var slotNode = slotNodes[i];
                ship.slots[i].SlotId = i;
                ship.slots[i].HasGun = false;
                ship.slots[i].MaxTier = int.Parse(slotNode["maxTier"].InnerText);
                ship.slots[i].positionOffset = new Vector2D(float.Parse(slotNode["position"]["x"].InnerText),
                                                            float.Parse(slotNode["position"]["y"].InnerText));
            }

            return ship;
        }

        public static ShipBaseData[] GetAllShips()
        {
            var ships = new List<ShipBaseData>();

            var doc = new XmlDocument();
            doc.LoadXml(Data.Ships);
            var nodes = doc.SelectNodes($"//ship");
            foreach (XmlNode nodeShip in nodes)
                ships.Add(GetShip(nodeShip["name"].InnerText));

            return ships.ToArray();
        }
    }
}