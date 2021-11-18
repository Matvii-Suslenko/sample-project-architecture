using System;
using System.Diagnostics;
using System.Threading;
using Core.Enums;
using Core.Helpers;

namespace Core.Game
{
    public class Ticker : ITicking, IUpdateable
    {
        public event Action<long> Tick = delegate (long tick) { };
        public event Action<UpdateType, long> UpdateEvent;

        public UpdateType UpdateType => UpdateType.TickerThread;

        private Thread _tickingThread;

        private int _tickDurationMs;

        private bool _toDispose;
        private bool _isPaused = true;

        private long _time;
        public long Time
        {
            get => _time;
            private set
            {
                try
                {
                    _time = value;
                    Tick(_time);
                    UpdateEvent(UpdateType, _time);
                }
                catch (Exception e)
                {
                    LoggerCore.SendLogException(e);
                }
            }
        }      

        public Ticker(long startTime, int tickDurationMs, string name)
        {
            _time = startTime;
            _tickDurationMs = tickDurationMs;
            _tickingThread = new Thread(Ticking);
            _tickingThread.Name = name;
            _tickingThread.Start();
        }

        public void Dispose()
        {
            _toDispose = true;
        }

        public void SetPause(bool pause)
        {
            _isPaused = pause;
        }

        private void Ticking()
        {
            Stopwatch watch = new Stopwatch();

            while (!_toDispose)
            {
                if (_isPaused)
                {
                    Thread.Sleep(_tickDurationMs);
                    continue;
                }

                watch.Restart();

                Time++;

                watch.Stop();

                int elapsedMs = (int)watch.ElapsedMilliseconds;

                if (elapsedMs >= _tickDurationMs)
                    continue;

                Thread.Sleep(_tickDurationMs - elapsedMs);
            }
        }
    }
}
