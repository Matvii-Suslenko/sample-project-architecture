using Core.Enums;

namespace Core.Serialization
{
    public interface ISerializable
    {
        int SerializeSize { get; }
        byte SerializeVersion { get; }
        SerializationType SerializationType { get; }
        int Serialize(byte[] destination, int offset);
        int Deserialize(byte[] source, int offset);
    }
}
