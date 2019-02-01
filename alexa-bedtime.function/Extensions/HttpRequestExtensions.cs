using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace alexa_bedtime.function.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> ReadBody<T>(this HttpRequest req)
        {
            var requestData = await req.ReadAsStringAsync();
            var requestBody = JsonConvert.DeserializeObject<T>(requestData);

            return requestBody;
        }
    }
}
