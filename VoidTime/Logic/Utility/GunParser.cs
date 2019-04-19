using System.Xml;
using VoidTime.Resources;

namespace VoidTime
{
    public static class GunParser
    {
        public static GunData GetGun(string name)
        {
            var doc = new XmlDocument();
            doc.LoadXml(Data.Guns);
            var node = doc.SelectSingleNode($"//gun[name=\'{name}\']");

            var gun = new GunData
            {
                CanRotate = bool.Parse(node["canRotate"].InnerText),
                CriticalChance = float.Parse(node["criticalChance"].InnerText),
                Damage = float.Parse(node["damage"].InnerText),
                FireRate = float.Parse(node["fireRate"].InnerText),
                Name = name,
                Price = int.Parse(node["price"].InnerText),
                Tier = int.Parse(node["tier"].InnerText),
                Range = float.Parse(node["range"].InnerText)
            };

            return gun;
        }
    }
}