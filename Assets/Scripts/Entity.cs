using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entities.Components;
using Core.Enums;
using Core.Helpers;
using Core.Serialization;

namespace Core.Entities
{
    public class Entity : IEnumerable<KeyValuePair<Type, IEntityComponent>>
    {
        private readonly Dictionary<Type, IEntityComponent> _components;

        public event Action<Entity> Unregister = delegate (Entity entity) { };

        public Entity()
        {
            _components = new Dictionary<Type, IEntityComponent>(16);
        }

        public void AddComponent<T>() where T : IEntityComponent, new() => AddComponent(new T());

        public void AddComponent<T>(T newComponent) where T : IEntityComponent
        {
            var componentType = typeof(T);

            if (_components.ContainsKey(componentType))
            {
                LoggerCore.SendLog($"AddComponent() Component of type: {componentType} is already present in entity.", LogType.Error);
                return;
            }

            _components.Add(componentType, newComponent);
        }

        public T GetComponent<T>() where T : IEntityComponent
        {
            try
            {
                return (T)_components[typeof(T)];
            }
            catch (KeyNotFoundException ex)
            {
                LoggerCore.SendLogException(ex);

                return default;
            }
        }

        public bool TryGetComponent<T>(out T component) where T : IEntityComponent
        {
            if (_components.TryGetValue(typeof(T), out IEntityComponent entityComponent))
            {
                component = (T)entityComponent;
                return true;
            }

            component = default;
            return false;
        }

        public void RemoveComponent<T>() where T : IEntityComponent
        {
            if (_components.TryGetValue(typeof(T), out IEntityComponent component) && _components.Remove(typeof(T)) && component is IDisposed disposableComponent)
                disposableComponent.Dispose();
        }

        public IEnumerator<KeyValuePair<Type, IEntityComponent>> GetEnumerator()
        {
            return _components.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int SerializeSize()
        {
            int size = 0;
            int typeSize = sizeof(SerializationType);

            size += sizeof(EntityId);//id

            foreach (KeyValuePair<Type, IEntityComponent> component in _components)
            {
                if (component.Value is ISerializable serializable)
                {
                    size += typeSize;
                    size += serializable.SerializeSize;
                }
            }

            size += typeSize; //SerializationType.End

            return size;
        }

        public void RaiseUnregister() => Unregister(this);
    }
}