using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using alexa_bedtime.function.Models;
using alexa_bedtime.function.Extensions;
using alexa_bedtime.function.Mappers;

namespace alexa_bedtime.function
{
    public static class WhiteNoiseFunction
    {
        [FunctionName("WhiteNoiseFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var mapper = new AlexaResponseMapper();

            var request = await req.ReadBody<AlexaRequest>();
            log.LogRequest(request);

            var result = request.HasStopIntent() ?
               mapper.MapStopRequest(request) :
               mapper.MapPlaybackResponse(request);

            return new OkObjectResult(result);

        }
    }
}
