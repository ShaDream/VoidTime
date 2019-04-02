using System.Drawing;

namespace VoidTime
{
    public class Camera : BasicCamera
    {
        public GameObject FollowTo;


        public Camera(Size size, GameObject gameObject)
        {
            Size = size;
            FollowTo = gameObject;
            Position = FollowTo.Position;
        }


        public override void Update()
        {
            Position = FollowTo.Position;
        }
    }
}