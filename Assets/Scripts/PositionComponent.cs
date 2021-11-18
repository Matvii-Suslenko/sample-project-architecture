using Core.Enums;
using Core.Helpers;
using Core.Serialization;

namespace Core.Entities.Components
{
    public class PositionComponent : IEntityComponent, ISerializable
    {
        public int SerializeSize =>
            sizeof(byte) + //Serialize version
            sizeof(double) * 2; //position

        public byte SerializeVersion => 0;
        public SerializationType SerializationType => SerializationType.Position;

        public Float3 Position;

        public PositionComponent(Float2 position = new Float2())
        {
            Position = position;
        }

        public int Serialize(byte[] destination, int offset)
        {
            int startOffset = offset;

            offset += ByteConverter.ToByteArray(SerializeVersion, destination, offset);
            offset += ByteConverter.ToByteArray(Position, destination, offset);

            return offset - startOffset;
        }

        public int Deserialize(byte[] source, int offset)
        {
            int startOffset = offset;

            offset += ByteConverter.FromByteArray(source, offset, out byte version);
            offset += ByteConverter.FromByteArray(source, offset, out Float2 position);
            Position = position;

            return offset - startOffset;
        }
    }
}
