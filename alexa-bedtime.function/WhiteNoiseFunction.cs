using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace alexa_bedtime.function
{
    public static class WhiteNoiseFunction
    {
        [FunctionName("WhiteNoiseFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            var requestData = req.ReadAsStringAsync().Result;
            log.LogInformation($"Received Request with data: {requestData}");

            if (requestData.Contains("AMAZON.StopIntent") || requestData.Contains("AMAZON.CancelIntent"))
            {
                var stopResult = GetStopResponse();

                var stopResponseData = JsonConvert.SerializeObject(stopResult);
                log.LogInformation($"Returning Stop Response: {stopResponseData}");

                return new OkObjectResult(stopResult);
            }

            var result = GetPlaybackResponse();

            var responseData = JsonConvert.SerializeObject(result);
            log.LogInformation($"Returning Playback Response: {responseData}");


            return new OkObjectResult(result);
        }

        private static dynamic GetStopResponse()
        {
            dynamic result = new ExpandoObject();

            result.version = "1.1";
            result.response = new ExpandoObject();
            result.response.shouldEndSession = true;
            result.response.card = new ExpandoObject();
            result.response.card.type = "Simple";
            result.response.card.title = "Bedtime";
            result.response.card.content = "Enjoy your day!";

            dynamic audioDirective = new ExpandoObject();
            audioDirective.type = "AudioPlayer.Stop";

            result.response.directives = new List<dynamic>
            {
                audioDirective
            };

            return result;
        }

        private static dynamic GetPlaybackResponse()
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
            audioDirective.audioItem.stream.url = "https://alexabedtime.blob.core.windows.net/sounds/10-hours-rain.mp3";
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
