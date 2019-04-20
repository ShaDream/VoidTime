namespace VoidTime
{
    public interface IItem
    {
        string Name { get; set; }

        int Price { get; set; }

        string GetInfo();
    }
}