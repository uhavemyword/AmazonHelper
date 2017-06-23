using log4net.Appender;
using log4net.Core;

namespace Common
{
    public delegate void LoggedEventHandler(LoggingEvent loggingEvent);

    public class MyLoggingAppender : AppenderSkeleton
    {
        public event LoggedEventHandler Logged;

        protected override void Append(LoggingEvent loggingEvent)
        {
            Logged?.Invoke(loggingEvent);
        }
    }
}