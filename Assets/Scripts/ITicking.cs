using System;

namespace Core.Game
{
    public interface ITicking
    {
        long Time { get; }

        event Action<long> Tick;

        void SetPause(bool pause);
        void Dispose();
    }
}