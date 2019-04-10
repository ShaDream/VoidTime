using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class BattleShipObject : PhysicalGameObject
    {
        public BattleShipStatsData Data;
        private Vector2D nextPosition;

        public void GetDamage(float damage)
        {
            damage = Data.Defence > damage ? 0 : damage - Data.Defence;
            Data.CurrentHealth -= damage;
            if (Data.CurrentHealth <= 0)
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