using System;
using Core.Enums;

namespace Core.Game
{
    public interface IUpdateable
    {
        event Action<UpdateType, long> UpdateEvent;
        UpdateType UpdateType { get; }
    }
}
