using System.Windows.Forms;

namespace VoidTime
{
    public class Player : GameObject
    {
        #region Public Properties

        public float Speed = 10;

        #endregion

        #region Constructor

        public Player(string nameObject) : base(nameObject)
        {
        }

        public Player(string nameObject, Vector2D position) : base(nameObject, position)
        {
        }

        public Player(string nameObject, Vector2D position, byte drawingPriority) : base(nameObject, position, drawingPriority)
        {
        }

        #endregion

        #region Public Methods

        public override void Update()
        {
            if (ReadonlyKeys.IsAnyKeyPressed(Keys.D, Keys.W, Keys.A, Keys.S))
                Position += new Vector2D(ReadonlyKeys.GetAxis("horizontal") * Speed,
                    ReadonlyKeys.GetAxis("vertical") * Speed);
        }

        #endregion

    }
}