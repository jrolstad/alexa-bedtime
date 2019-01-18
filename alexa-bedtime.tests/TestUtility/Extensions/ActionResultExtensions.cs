using Microsoft.AspNetCore.Mvc;

namespace alexa_bedtime.tests.TestUtility.Extensions
{
    public static class ActionResultExtensions
    {
        public static T Value<T>(this IActionResult result)
        {
            var objectResult = (ObjectResult) result;

            var value = (T) objectResult.Value;

            return value;

        }
    }
}