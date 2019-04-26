using System;
using System.Collections.Generic;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public static class PhysicsBodyRemover
    {
        private static readonly Dictionary<World, List<Tuple<Body, IEnumerable<Fixture>>>> removeBodyList =
            new Dictionary<World, List<Tuple<Body, IEnumerable<Fixture>>>>();

        public static void RemoveBodyies(World world)
        {
            if (!removeBodyList.ContainsKey(world))
                return;

            foreach (var item in removeBodyList[world])
            {
                foreach (var fixture in item.Item2)
                    item.Item1.DestroyFixture(fixture);
                item.Item1.World.DestroyBody(item.Item1);
            }

            removeBodyList.Remove(world);
        }

        public static void Add(Body body, IEnumerable<Fixture> fixtures)
        {
            if (body == null)
                return;
            if (!removeBodyList.ContainsKey(body.World))
                removeBodyList[body.World] = new List<Tuple<Body, IEnumerable<Fixture>>>();
            removeBodyList[body.World].Add(Tuple.Create(body, fixtures));
        }
    }
}