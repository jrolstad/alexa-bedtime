using System;
using System.Linq;
using alexa_bedtime.function.Models;

namespace alexa_bedtime.function.Extensions
{
    public static class AlexaRequestExtensions
    {
        public static string Intent(this AlexaRequest request)
        {
            var intent = request?.request?.intent?.name ?? "";

            return intent;
        }
        public static bool HasStopIntent(this AlexaRequest request)
        {
            var recognizedStopIntents = new[]
            {
                "AMAZON.StopIntent",
                "AMAZON.CancelIntent",
                "AMAZON.PauseIntent",
            };

            var intent = request.Intent();
            var isStopIntent = recognizedStopIntents.Contains(intent, StringComparer.InvariantCultureIgnoreCase);

            return isStopIntent;
        }
    }
}
