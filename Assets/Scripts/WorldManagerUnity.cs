using Components;
using Core.Data.World;
using Core.Entities;
using Core.Game;
using Events;

namespace Game
{
    public class WorldManagerUnity : WorldManagerCore
    {
        UnityEvents _unityEvents;

        public WorldManagerUnity(WorldData worldData, UnityEvents unityEvents) : base (worldData)
        {
            _unityEvents = unityEvents;
            EntityManager.AddSystem(new EntityRenderingSystem(EntityManager, unityEvents));
        }
    }
}
