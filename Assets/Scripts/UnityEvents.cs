using System;
using Core;
using Core.Enums;
using Core.Game;
using Core.Helpers;
using UnityEngine;

namespace Events
{
    public class UnityEvents : MonoBehaviour, IUpdateable
    {
        public event Action OnLateUpdate = delegate { };
        public event Action OnUpdate = delegate { };
        public event Action<UpdateType, long> UpdateEvent = delegate { };

        public float Time { get; private set; }
        public float DeltaTime { get; private set; }

        public UpdateType UpdateType => UpdateType.MainThread;

        public bool IsPaused = true;

        private void Update()
        {
            if (IsPaused)
                return;

            Time = UnityEngine.Time.time;
            DeltaTime = UnityEngine.Time.deltaTime;

            try
            {
                OnUpdate();
                UpdateEvent(UpdateType, (long)Time);
            }
            catch (Exception e)
            {
                LoggerCore.SendLogException(e);
            }
        }

        private void LateUpdate()
        {
            if (IsPaused)
                return;

            OnLateUpdate();
        }
    }
}
