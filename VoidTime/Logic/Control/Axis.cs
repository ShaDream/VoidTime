using System.Windows.Forms;

namespace VoidTime
{
    public class Axis
    {
        #region Public Properties

        public string Name { get; }
        public Keys PositiveKey { get; }
        public Keys NegativeKey { get; }

        #endregion

        #region Public Methods

        public Axis(string name, Keys positiveKey, Keys negativeKey)
        {
            Name = name;
            PositiveKey = positiveKey;
            NegativeKey = negativeKey;
        }

        public float GetValue(bool isPositiveKeyPressed, bool isNegativeKeyPressed)
        {
            var result = 0f;
            result += isPositiveKeyPressed ? 1 : 0;
            result -= isNegativeKeyPressed ? 1 : 0;
            return result;
        }

        #endregion

    }
}