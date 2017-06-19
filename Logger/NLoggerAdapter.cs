using Logger.Interfaces;
using System;
using NLog;


namespace Logger
{
    public class NLoggerAdapter : Logger.Interfaces.ILogger
    {
        private readonly NLog.Logger logger;

        public NLoggerAdapter(NLog.Logger logger)
        {
            if (ReferenceEquals(logger, null))
                throw new ArgumentNullException($"{nameof(logger)} is null.");

            this.logger = logger;
        }

        public void Trace(string message) => logger.Trace(message);

        public void Trace(Exception exception, string message, params object[] args) => logger.Trace(exception, message, args);

        public void Trace(string message, params object[] args) => logger.Trace(message, args);

        public void Debug(string message) => logger.Debug(message);

        public void Debug(Exception exception, string message, params object[] args) => logger.Debug(exception, message, args);

        public void Debug(string message, params object[] args) => logger.Debug(message, args);

        public void Info(string message) => logger.Info(message);

        public void Info(Exception exception, string message, params object[] args) => logger.Info(exception, message, args);

        public void Info(string message, params object[] args) => logger.Info(message, args);

        public void Warn(string message) => logger.Warn(message);

        public void Warn(Exception exception, string message, params object[] args) => logger.Warn(exception, message, args);

        public void Warn(string message, params object[] args) => logger.Warn(message, args);

        public void Error(string message) => logger.Error(message);

        public void Error(Exception exception, string message, params object[] args) => logger.Error(exception, message, args);

        public void Error(string message, params object[] args) => logger.Error(message, args);

        public void Fatal(string message) => logger.Fatal(message);

        public void Fatal(Exception exception, string message, params object[] args) => logger.Fatal(exception, message, args);

        public void Fatal(string message, params object[] args) => logger.Fatal(message, args);

    }
}
