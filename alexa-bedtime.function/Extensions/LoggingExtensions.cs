using System;
using System.Collections.Generic;
using System.Linq;
using alexa_bedtime.function.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace alexa_bedtime.function.Extensions
{
    public static class LoggingExtensions
    {
        public static void LogRequest(this ILogger logger, AlexaRequest request)
        {
            var requestBody = JsonConvert.SerializeObject(request);

            var values = new Dictionary<string, object>
            {
                {"action","alexa-request"},
                {"body",requestBody},
                {"intent",request.Intent()},
                {"sessionId",request?.session?.sessionId},
                {"application",request?.session?.application?.applicationId},
                {"userId",request?.session.user?.userId},
                {"deviceId",request?.context?.System?.device?.deviceId}
            };

            logger.LogTraceWithValues("Received request", values);

        }

        public static void LogResponse(this ILogger logger, AlexaRequest request, AlexaResponse response)
        {
            var responseBody = JsonConvert.SerializeObject(response);

            var directives = response?
                .response?
                .directives?
                .Select(d => d.type);
            var directivesString = string.Join("|", directives);

            var values = new Dictionary<string, object>
            {
                {"action","alexa-response"},
                {"body",responseBody},
                {"sessionId",request?.session?.sessionId},
                {"directives",directivesString}
            };

            logger.LogTraceWithValues("Returning response", values);
        }

        public static void LogTraceWithValues(this ILogger logger, string message, Dictionary<string, object> values)
        {
            logger.Log(LogLevel.Trace, new EventId(), values, null, (Dictionary<string, object> arg1, Exception arg2) => message);
        }

    }
}
