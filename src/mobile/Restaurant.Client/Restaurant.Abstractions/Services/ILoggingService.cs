using System;

namespace Restaurant.Abstractions.Services
{
    public interface ILoggingService
    {
        void Info(string message);
        void Error(string message);
        void Error(Exception ex);
        void Error(Exception e, string message);
        void Debug(string message);
        void Trace(string message);
        void Warn(string message);
    }
}
