using System;
using NLog;
using Restaurant.Abstractions.Services;

namespace Restaurant.Droid
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception ex)
        {
            _logger.Error(ex);
        }

        public void Error(Exception e, string message)
        {
            _logger.Error(e, message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }
    }
}
