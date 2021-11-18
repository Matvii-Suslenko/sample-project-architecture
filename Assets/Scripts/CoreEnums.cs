namespace Core.Enums
{
    public enum SerializationType : ushort
    {
        None = 0,
        EntityData = 1,
        Position = 2,
        End = ushort.MaxValue,
    }

    public enum UpdateType : byte
    {
        MainThread = 0,
        TickerThread = 1,
    }
}