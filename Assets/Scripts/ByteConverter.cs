using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Core.Entities;
using Core.Entities.Components;
using Core.Enums;
using Core.Serialization;

namespace Core.Helpers
{
    public static partial class ByteConverter
    {
        #region To

        public static int ToByteArray(Float2 source, byte[] destination, int offset)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(source.x), 0, destination, offset, sizeof(float));
            Buffer.BlockCopy(BitConverter.GetBytes(source.y), 0, destination, offset + sizeof(float), sizeof(float));
            return sizeof(float) * 2;
        }

        public static int ToByteArray(Int2 vector, Stream destination)
        {
            int size = 0;
            size += ToByteArray(vector.x, destination);
            size += ToByteArray(vector.y, destination);
            return size;
        }

        public static int ToByteArray(Entity entity, byte[] destination, int offset)
        {
            int startOffset = offset;

            foreach (var componentTypeAndValue in entity)
            {
                if (componentTypeAndValue.Value is ISerializable serializableComponent)
                {
                    offset += ToByteArray((ushort)serializableComponent.SerializationType, destination, offset);
                    offset += serializableComponent.Serialize(destination, offset);
                }
            }
            offset += ToByteArray((ushort)SerializationType.End, destination, offset);

            return offset - startOffset;
        }

        #endregion

        #region From

        public static int FromByteArray(byte[] source, int offset, out Float2 destination)
        {
            destination = new Float2(BitConverter.ToSingle(source, offset), BitConverter.ToSingle(source, offset + sizeof(float)));
            return sizeof(float) * 2;
        }

        public static int FromByteArray(byte[] source, int offset, out Int2 destination)
        {
            int index = offset;
            index += FromByteArray(source, index, out destination.x);
            index += FromByteArray(source, index, out destination.y);
            return index - offset;
        }

        public static int FromByteArray(byte[] source, int offset, Entity entity)
        {
            int index = offset;

            bool componentsEndReached = false;
            while (!componentsEndReached)
            {
                index += FromByteArray(source, index, out ushort serializedTypeShort);
                var serializedType = (SerializationType)serializedTypeShort;

                switch (serializedType)
                {
                    case SerializationType.Position:
                        if (!entity.TryGetComponent(out PositionComponent position))
                            entity.AddComponent(position = new PositionComponent());
                        index += position.Deserialize(source, index);
                        break;     
                    case SerializationType.End:
                        componentsEndReached = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Fatal error during deserialization of entity with Id: {entity.GetComponent<EntityDataComponent>().Id} Component of type: {serializedType}!");
                }
            }

            return index - offset;
        }

        #endregion
    }
}
