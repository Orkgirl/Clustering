using System;
using Assets.Scripts.Entity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Managers.Logger
{
    public class LoggerManager : IEntity, ILogger
    {
        private UnityEngine.Logger _logger;

        private string _layoutName = "LoggerLayout";
        private UIItem _layoutGameObject;

        public void Install()
        {
            _layoutGameObject = GameObject.Find(_layoutName).GetComponent<UIItem>();
            _logger = new UnityEngine.Logger(new LoggerHelper());
        }

        public void Initialaze()
        {

        }

        public ILogHandler logHandler { get; set; }
        public bool logEnabled { get; set; }
        public LogType filterLogType { get; set; }

        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            
        }

        public void LogException(Exception exception, Object context)
        {
            throw new NotImplementedException();
        }

        public bool IsLogTypeAllowed(LogType logType)
        {
            throw new NotImplementedException();
        }

        public void Log(LogType logType, object message)
        {
            throw new NotImplementedException();
        }

        public void Log(LogType logType, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public void Log(LogType logType, string tag, object message)
        {
            throw new NotImplementedException();
        }

        public void Log(LogType logType, string tag, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public void Log(object message)
        {
            throw new NotImplementedException();
        }

        public void Log(string tag, object message)
        {
            throw new NotImplementedException();
        }

        public void Log(string tag, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string tag, object message)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string tag, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public void LogError(string tag, object message)
        {
            throw new NotImplementedException();
        }

        public void LogError(string tag, object message, Object context)
        {
            throw new NotImplementedException();
        }

        public void LogFormat(LogType logType, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void LogException(Exception exception)
        {
            throw new NotImplementedException();
        }
    }

    public class LoggerHelper : ILogHandler
    {
        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            Debug.logger.logHandler.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            Debug.logger.LogException(exception, context);
        }
    }
}
