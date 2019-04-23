namespace VoidTime
{
    public struct ShipSlotData
    {
        /// <summary>
        ///     Offset from center of object
        /// </summary>
        public Vector2D positionOffset;

        public int SlotId;
        public int MaxTier;
        public bool HasGun;
        public GunData Gun;

        public bool IsCanAdd(GunData gun)
        {
            return MaxTier >= gun.Tier;
        }
    }
}