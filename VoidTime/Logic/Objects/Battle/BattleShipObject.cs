using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;

namespace VoidTime.Logic.Objects
{
    public class BattleShipObject : PhysicalGameObject
    {
        public BattleShipStatsData data;

        public void GetDamage(float damage)
        {
            damage = data.Defence > damage ? 0 : damage - data.Defence;
            data.CurrentHealth -= damage;
            if (data.CurrentHealth <= 0)
                Destoy();
        }

        public override void CreatePhysics(World world)
        {
            var bodyDef = CreateBodyDef();
            bodyDef.FixedRotation = true;
            bodyDef.Position = ConvertToPhysicsVector(Position);
            bodyDef.BodyType = BodyType.DynamicBody;

            var shape = new PolygonShape();
            shape.SetAsBox(30 * ScaleFactor, 40 * ScaleFactor);
            Body = world.CreateBody(bodyDef);
            Fixtures.Add(Body.CreateFixture(shape, 1));
        }
    }
}