using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FruitsStore_API.Services
{
    public class LoggerService
    {
        private readonly string logFilePath;

        private Guid _guid;
        public LoggerService()
        {
            try
            {
                string logsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(logsFolder))
                {
                    Directory.CreateDirectory(logsFolder);
                }

                logFilePath = Path.Combine(logsFolder, "log.txt");

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
            }
            catch (Exception ex)
            {
                LogError("Error in LoggerService", ex);
                throw ex;
            }
        }
        public void StartRequest()
        {
            _guid = Guid.NewGuid();
        }
        public void LogInformation(string message)
        {
            Log.Information($"[{_guid}] - {message}");
        }

        public void LogError(string message, Exception ex)
        {
            Log.Error(ex, $"[{_guid}] - {message}");
        }
    }
}