using Core.Data.World;
using Core.Entities;
using Core.Enums;

namespace Core.Game
{
    public class WorldManagerCore
    {
        protected readonly ITicking Ticker;
        protected readonly WorldData WorldData;
        protected readonly EntityManager EntityManager;

        public readonly long TicksToSave = 120 * CoreConstants.TicksPerSecond;

        public WorldManagerCore(WorldData worldData)
        {
            WorldData = worldData;
            Ticker = new Ticker(worldData.Time, CoreConstants.TickDurationMsInt, "Core ticker");
            Ticker.Tick += OnTick;

            EntityManager = new EntityManager(Ticker);
            EntityManager.RegisterEntity(EntityType.DynamicObject, EntityFactory.CreateEntity(EntityId.Box));
        }

        private void OnTick(long tick)
        {
            if (tick % TicksToSave == 0)
            {
                SerializeGame();
            }
        }

        public virtual void SerializeGame()
        {
            
        }
    }
}
