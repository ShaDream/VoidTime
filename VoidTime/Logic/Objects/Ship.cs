using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class Ship : PhysicalGameObject
    {
        public InstalledChips Chips;
        public ShipData Data;
        public Inventory Inventory;


        public override void CreatePhysics(World world)
        {
            var bodyDef = CreateBodyDef();
            bodyDef.FixedRotation = true;
            bodyDef.Position = ConvertToPhysicsVector(Position);
            bodyDef.BodyType = BodyType.DynamicBody;

            var shape = new PolygonShape();
            shape.SetAsBox(30 * ScaleFactor, 30 * ScaleFactor);
            Body = world.CreateBody(bodyDef);
            Fixtures.Add(Body.CreateFixture(shape, 1));
        }
    }
}