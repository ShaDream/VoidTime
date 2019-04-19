namespace VoidTime
{
    public static class Random
    {
        private static readonly System.Random Rng = new System.Random();

        public static bool IsLucky(float chance)
        {
            return Rng.NextDouble() * 100 < chance;
        }
    }
}