namespace VoidTime
{
    public class ShipBaseData : IItem
    {
        public string Name { get; set; }
        public float HP;
        public float Defence;
        public float MoveSpeed;
        public ShipSlotData[] slots;
        public int Price { get; set; }

        public string GetInfo()
        {
            return $"{Name}\n" +
                   $"HP: {HP}\n" +
                   $"Defence: {Defence}\n" +
                   $"Speed: {MoveSpeed}\n" +
                   $"Slots count: {slots.Length}\n" +
                   $"Price: {Price}\n";
        }
    }
}