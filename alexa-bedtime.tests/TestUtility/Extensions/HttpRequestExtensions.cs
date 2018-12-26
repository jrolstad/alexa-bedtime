using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace alexa_bedtime.tests.TestUtility.Extensions
{
    public static class HttpRequestExtensions
    {
        public static HttpRequest WithPostRequest(this TestCompositionRoot root, string body)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());

            var byteArray = Encoding.ASCII.GetBytes(body);
            var stream = new MemoryStream(byteArray);

            request.Body = stream;

            return request;
        }
    }
}