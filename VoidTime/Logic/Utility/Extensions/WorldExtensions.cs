using Box2DSharp.Dynamics;

namespace VoidTime
{
    public static class WorldExtensions
    {
        public static void StepWithDelete(this World world, float timeStep, int velocityIterations,
                                          int positionIterations)
        {
            world.Step(timeStep, velocityIterations, positionIterations);
            PhysicsBodyRemover.RemoveBodyies(world);
        }
    }
}