using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace alexa_bedtime.tests.TestUtility.Extensions
{
    public static class HttpRequestExtensions
    {
        public static HttpRequest WithPostRequest(this TestCompositionRoot root, Stream body)
        {
            body.Position = 0;
            var request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                Body = body
            };


            return request;
        }
    }
}