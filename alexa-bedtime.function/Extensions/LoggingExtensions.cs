using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace alexa_bedtime.function.Extensions
{
    public static class LoggingExtensions
    {
        public static void LogTrace(this ILogger logger, string message, Dictionary<string,object> values)
        {
            logger.Log(LogLevel.Trace, new EventId(), values, null, (Dictionary<string, object> arg1, Exception arg2) => message);
        }
    }
}
