using Microsoft.Extensions.Logging;
using System;
using System.IO;
using DownTime.CommonModel.Models;
using DownTime.Services.Interface;

namespace DownTime.Logger
{
    public class FileLogger : ILogger
    {
        private readonly INotificationService _notificationService;
        public FileLogger(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public FileLogger()
        {
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.Error || logLevel == LogLevel.Critical || logLevel == LogLevel.Warning || logLevel==LogLevel.Trace)
            {          
                var message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)} - {exception?.StackTrace}";
                WriteMessageToFile(message);

                // And email send.
                _notificationService.EmailSend(new EmailSendModel
                {
                    Subject = "DownSite Alerter " + logLevel,
                    To = "ahmet.sonmez37@gmail.com", // or control with parameter admin email adress
                    Body = "Exception Message : " + exception?.Message + "Exception Detail : " + exception?.StackTrace,
                    DisplayName = "DownSite Alerter Site Message"
                });
            }            
        }

        private static void WriteMessageToFile(string message)
        {           
            const string filePath = "C:\\AspCoreFileLog.txt";
            if (!File.Exists(filePath))
            {
                var sr = File.CreateText(filePath);
                sr.Close();
            }
            using (var sw = File.AppendText(filePath))
            {
                sw.WriteLine(message);
                sw.Close();
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
