using Box2DSharp.Collision.Collider;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace VoidTime
{
    public class GlobalContactListner : IContactListener
    {
        public void BeginContact(Contact contact)
        {
            (contact.FixtureA.Body.UserData as PhysicalGameObject)?.BeginContact(contact);
            (contact.FixtureB.Body.UserData as PhysicalGameObject)?.BeginContact(contact);
        }

        public void EndContact(Contact contact)
        {
            (contact.FixtureA.Body.UserData as PhysicalGameObject)?.EndContact(contact);
            (contact.FixtureB.Body.UserData as PhysicalGameObject)?.EndContact(contact);
        }

        public void PreSolve(Contact contact, in Manifold oldManifold)
        {
            (contact.FixtureA.Body.UserData as PhysicalGameObject)?.PreSolve(contact, oldManifold);
            (contact.FixtureB.Body.UserData as PhysicalGameObject)?.PreSolve(contact, oldManifold);
        }

        public void PostSolve(Contact contact, in ContactImpulse impulse)
        {
            (contact.FixtureA.Body.UserData as PhysicalGameObject)?.PostSolve(contact, impulse);
            (contact.FixtureB.Body.UserData as PhysicalGameObject)?.PostSolve(contact, impulse);
        }
    }
}