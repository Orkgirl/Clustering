using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Managers.Logger;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Common
{
    public static class Logger
    {
        private static bool isInitialized;

        private static LoggerManager _loggerManager;

        public static void Initialize(LoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
            isInitialized = true;
        }

        public static ILogHandler logHandler { get; set; }
        public static bool logEnabled { get; set; }
        public static LogType filterLogType { get; set; }

        public static void LogFormat(LogType logType, Object context, string format, params object[] args)
        {

        }

        public static void LogException(Exception exception, Object context)
        {
            throw new NotImplementedException();
        }

        public static bool IsLogTypeAllowed(LogType logType)
        {
            throw new NotImplementedException();
        }

        public static void Log(LogType logType, object message)
        {
            throw new NotImplementedException();
        }

        public static void Log(LogType logType, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public static void Log(LogType logType, string tag, object message)
        {
            throw new NotImplementedException();
        }

        public static void Log(LogType logType, string tag, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public static void Log(object message)
        {
            throw new NotImplementedException();
        }

        public static void Log(string tag, object message)
        {
            throw new NotImplementedException();
        }

        public static void Log(string tag, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public static void LogWarning(string tag, object message)
        {
            throw new NotImplementedException();
        }

        public static void LogWarning(string tag, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public static void LogError(string tag, object message)
        {
            throw new NotImplementedException();
        }

        public static void LogError(string tag, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public static void LogFormat(LogType logType, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public static void LogException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
