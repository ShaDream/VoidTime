namespace VoidTime.GUI
{
    public interface IDrawData
    {
        UIMask Mask { get; set; }
        float DrawPriority { get; set; }
    }
}