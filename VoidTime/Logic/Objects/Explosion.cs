using System.Drawing;

namespace VoidTime
{
    public class Explosion : GameObject
    {
        private int frame;

        public Explosion(Vector2D position)
        {
            Position = position;
        }

        public override void Update()
        {
            frame++;
            if (frame > 110)
                Destoy();
        }
    }
}