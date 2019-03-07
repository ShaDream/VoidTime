using System.Windows.Forms;

namespace VoidTime
{
    public class Player : GameObject
    {
        public Player(string nameObject) : base(nameObject)
        {
        }

        public Player(string nameObject, Vector2D position) : base(nameObject, position)
        {
        }

        public Player(string nameObject, Vector2D position, byte drawingPriority) : base(nameObject, position, drawingPriority)
        {
        }

        public override void Update()
        {
            if (ReadonlyKeys.keys.keys.Contains(Keys.D))
                Position += new Vector2D(10, 0);
            if (ReadonlyKeys.keys.keys.Contains(Keys.A))
                Position += new Vector2D(-10, 0);
            if (ReadonlyKeys.keys.keys.Contains(Keys.W))
                Position += new Vector2D(0, 10);
            if (ReadonlyKeys.keys.keys.Contains(Keys.S))
                Position += new Vector2D(0, -10);
        }
    }
}