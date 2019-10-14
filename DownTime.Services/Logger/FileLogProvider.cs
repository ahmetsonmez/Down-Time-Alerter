using DownTime.Services.Concrete;
using Microsoft.Extensions.Logging;

namespace DownTime.Services.Logger
{
    public class FileLogProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string category)
        {
            return new FileLogger(new NotificationService());
        }
        public void Dispose()
        {

        }
    }
}
