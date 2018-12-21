using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace alexa_bedtime.function
{
    public static class WhiteNoiseFunction
    {
        [FunctionName("WhiteNoiseFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var requestData = req.ReadAsStringAsync().Result;

            if (requestData.Contains("AMAZON.StopIntent") || requestData.Contains("AMAZON.CancelIntent"))
            {
                var stopResult = GetStopResponse();

                return new OkObjectResult(stopResult);
            }

            var result = GetAudioResponse();

            return new OkObjectResult(result);
        }

        private static dynamic GetStopResponse()
        {
            dynamic stopResult = new ExpandoObject();

            stopResult.version = "1.1";
            stopResult.response = new ExpandoObject();
            stopResult.response.shouldEndSession = true;
            stopResult.response.card = new ExpandoObject();
            stopResult.response.card.type = "Simple";
            stopResult.response.card.title = "AMAZON.CancelIntent";
            stopResult.response.card.content = "Thank you for using bedtime.  Goodbye";
            return stopResult;
        }

        private static dynamic GetAudioResponse()
        {
            dynamic result = new ExpandoObject();
            result.version = "1.0";

            result.response = new ExpandoObject();
            result.response.shouldEndSession = true;

            dynamic audioDirective = new ExpandoObject();
            audioDirective.type = "AudioPlayer.Play";
            audioDirective.playBehavior = "REPLACE_ALL";
            audioDirective.audioItem = new ExpandoObject();
            audioDirective.audioItem.stream = new ExpandoObject();
            audioDirective.audioItem.stream.url ="https://alexabedtime.blob.core.windows.net/sounds/Light-rain-sound-effect.mp3";
            audioDirective.audioItem.stream.token = "0";
            audioDirective.audioItem.stream.offsetInMilliseconds = 0;
            result.response.directives = new List<dynamic>
            {
                audioDirective
            };
            return result;
        }
    }
}