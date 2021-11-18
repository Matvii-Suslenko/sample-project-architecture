using System;

namespace Core.Helpers
{
    public enum LogType
    {
        Log,
        Warning,
        Error,
    }

    public struct LogData
    {
        public string Message;
        public LogType Type;
        public string StackTrace;

        public LogData(object message, LogType logType)
        {
            Message = message.ToString();
            Type = logType;
            StackTrace = Environment.StackTrace;
        }
    }

    public static class LoggerCore
    {
        public static event Action<LogData> Log = delegate (LogData logData) { };

        public static void SendLog(string message, LogType logType = LogType.Log) => SendLog(new LogData() { Message = message, Type = logType });
        public static void SendLog(object message, LogType logType = LogType.Log) => SendLog(new LogData() { Message = message.ToString(), Type = logType });

        public static void SendLog(LogData data)
        {
            Log(data);
        }

        public static void SendLogException(Exception exception)
        {
            Log(new LogData { Message = exception.Message, StackTrace = exception.StackTrace, Type = LogType.Error });
        }
    }
}
