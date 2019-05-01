using System;
using System.Xml;
using VoidTime.Resources;

namespace VoidTime
{
    public static class EnemyCreator
    {
        public static BattleEnemy CreateEnemy(EnemyDifficult difficult, EnemyBehaviorType behavior)
        {
            var enemy = new BattleEnemy();

            var doc = new XmlDocument();
            doc.LoadXml(Data.Enemies);
            var d = difficult.GetDescription();
            var nodes = doc.SelectNodes($"//enemy[difficult=\'{difficult.GetDescription()}\' and behavior=\'{behavior.GetDescription()}\' ]");
            var node = nodes[Random.Next(nodes.Count)];

            enemy.Data.SetShip(ShipParser.GetShip(node["ship"].InnerText));
            for (var i = 0; i < node["guns"].ChildNodes.Count; i++)
            {
                var gun = node["guns"].ChildNodes[i];
                enemy.Data.SetGun(GunParser.GetGun(gun.InnerText), i);
            }

            enemy.behavior = (EnemyBehaviorType)Enum.Parse(typeof(EnemyBehaviorType),
                                                            node["behavior"].InnerText.Replace(" ", ""),
                                                            true);

            //TODO: Create chips
            return enemy;
        }
    }
}