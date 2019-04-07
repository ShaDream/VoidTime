using System;
using System.Collections.Generic;
using System.Drawing;
using Box2DSharp.Dynamics;

namespace VoidTime
{
    public class Chunk
    {
        private readonly List<GameObject> gameObjects = new List<GameObject>();

        public Point Coordinates { get; }
        public Size Size { get; }


        public Chunk() { }

        public Chunk(Point coordinates, Size size)
        {
            Coordinates = coordinates;
            Size = size;
            gameObjects.Add(new Background
            {
                Position = new Vector2D(Coordinates.X * Size.Width, Coordinates.Y * Size.Height + Size.Height - 1),
                DrawingPriority = 2
            });
        }


        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            gameObject.OnDestroy += RemoveGameObject;
            gameObject.OnCreate += OnObjectCreate;
        }

        public void AddGameObjects(IEnumerable<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                this.gameObjects.Add(gameObject);
                gameObject.OnDestroy += RemoveGameObject;
            }
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
            gameObject.OnDestroy -= RemoveGameObject;
            gameObject.OnCreate -= OnObjectCreate;
        }

        public void ClearPhysicsObjects()
        {
            foreach (var gameObject in gameObjects)
                if (gameObject is PhysicalGameObject o)
                    o.DeletePhysics();
        }

        public void ResumePhysics(World world)
        {
            foreach (var gameObject in gameObjects)
                if (gameObject is PhysicalGameObject o)
                    o.CreatePhysics(world);
        }

        public List<GameObject> GetGameObjects()
        {
            return gameObjects;
        }

        public Action<GameObject> OnObjectCreate;
    }
}