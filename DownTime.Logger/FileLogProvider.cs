using Microsoft.Extensions.Logging;

namespace DownTime.Logger
{
    public class FileLogProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string category)
        {
            return new FileLogger();
        }
        public void Dispose()
        {

        }
    }
}
