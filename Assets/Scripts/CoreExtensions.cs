using Core.Entities;

namespace Core.Helpers
{
    public static class CoreExtensions
    {
        public static bool HasEntityType(this EntityType types, EntityType type)
        {
            return (types & type) == type;
        }
    }    
}