using System.Drawing;

namespace VoidTime
{
    public class Camera : BasicCamera
    {

        #region Public Properties

        public GameObject FollowTo;

        #endregion

        #region Constructor

        public Camera(Size size, GameObject gameObject)
        {
            Size = size;
            FollowTo = gameObject;
            Position = FollowTo.Position;
        }

        #endregion

        #region Public Methods

        public override void Update()
        {
            Position = FollowTo.Position;
        }

        #endregion

    }
}