using System.Xml;

namespace VoidTime
{
    public static class ShipParser
    {
        //TODO:Check for exception
        public static ShipBaseData GetShip(string name)
        {
            var doc = new XmlDocument();
            doc.LoadXml(Resources.Data.Ships);
            var node = doc.SelectSingleNode($"//ship[name=\'{name}\']");

            var ship = new ShipBaseData
            {
                ShipName = name,
                HP = float.Parse(node["hp"].InnerText),
                Defence = float.Parse(node["defence"].InnerText),
                MoveSpeed = float.Parse(node["speed"].InnerText),
                slots = new ShipSlotData[node["slots"].ChildNodes.Count]
            };

            var slotNodes = doc.SelectNodes($"//ship[name=\'{name}\']//slots//slot");

            for (var i = 0; i < slotNodes.Count; i++)
            {
                var slotNode = slotNodes[i];
                ship.slots[i].HasGun = false;
                ship.slots[i].MaxTier = int.Parse(slotNode["maxTier"].InnerText);
                ship.slots[i].positionOffset = new Vector2D(float.Parse(node["position"]["x"].InnerText),
                    float.Parse(node["position"]["y"].InnerText));
            }

            return ship;
        }
    }
}