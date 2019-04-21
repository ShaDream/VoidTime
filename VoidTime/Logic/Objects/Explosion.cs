using System.Drawing;

namespace VoidTime
{
    public class Explosion : GameObject
    {
        public int frame;

        public Explosion(Vector2D position)
        {
            Position = position;
        }

        public override void Update()
        {
                frame++;
            if (frame > 109)
                Destoy();
        }
    }
}