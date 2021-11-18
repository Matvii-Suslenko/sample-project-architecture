using System;
using Core.Entities.Components;
using Core.Enums;
using Core.Helpers;
using Core.Serialization;

namespace Core.Entities.Components
{
    public class EntityDataComponent : IEntityComponent, ISerializable
    {
        public EntityId Id;
        public Guid Guid;

        public int SerializeSize =>
            sizeof(byte) + //Serialize version
            sizeof(EntityId) +
            CoreConstants.GuidSize;

        public byte SerializeVersion => 0;
        public SerializationType SerializationType => SerializationType.EntityData;

        public EntityDataComponent() { }
        public EntityDataComponent(EntityId id, Guid guid = default(Guid))
        {
            Id = id;
            if (guid == default)
                guid = Guid.NewGuid();
            Guid = guid;
        }

        public int Serialize(byte[] destination, int offset)
        {
            int startOffset = offset;
            offset += ByteConverter.ToByteArray(SerializeVersion, destination, offset);
            offset += ByteConverter.ToByteArray((ushort)Id, destination, offset);
            offset += ByteConverter.ToByteArray(Guid, destination, offset);
            return offset - startOffset;
        }

        public int Deserialize(byte[] source, int offset)
        {
            int startOffset = offset;

            offset += ByteConverter.FromByteArray(source, offset, out byte version);
            offset += ByteConverter.FromByteArray(source, offset, out ushort id);
            Id = (EntityId)id;
            offset += ByteConverter.FromByteArray(source, offset, out Guid);

            return offset - startOffset;
        }
    }
}
