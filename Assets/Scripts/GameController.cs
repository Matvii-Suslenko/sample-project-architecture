using Core.Helpers;
using Core.Data.World;
using Game;
using UnityEngine;
using Events;

public class GameController : MonoBehaviour
{
    private WorldManagerUnity _worldManager;

    [SerializeField] private UnityEvents _unityEvents;

    private void Awake()
    {
        LoggerCore.Log += OnLog;
    }

    private void Start()
    {
        _worldManager = new WorldManagerUnity(new WorldData(), _unityEvents);
    }

    private void OnDestroy()
    {
        LoggerCore.Log -= OnLog;
    }

    private void OnLog(LogData data)
    {
        var message = $"Message: {data.Message}\nStackTrace: {data.StackTrace}";
        switch (data.Type)
        {
            case Core.Helpers.LogType.Log:
                Debug.Log(message);
                break;
            case Core.Helpers.LogType.Warning:
                Debug.LogWarning(message);
                break;
            case Core.Helpers.LogType.Error:
                Debug.LogError(message);
                break;
        }
    }
}
