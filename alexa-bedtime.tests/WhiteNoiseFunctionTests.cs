using System;
using System.Threading.Tasks;
using alexa_bedtime.function;
using alexa_bedtime.tests.TestUtility;
using alexa_bedtime.tests.TestUtility.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace alexa_bedtime.tests
{
    public class WhiteNoiseFunctionTests
    {
        [Fact]
        public async Task Run_StartIntent_ReturnsAudioPlayerResult()
        {
            // Arrange
            var root = TestCompositionRoot.Create();

            var request = root.WithPostRequest("");
            var logger = root.Get<ILogger>();
            
            // Act
            var result =  await WhiteNoiseFunction.Run(request, logger);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var typedResult = (OkObjectResult) result;

            dynamic resultData = typedResult.Value;

            Assert.Equal("1.0",resultData.version);
            Assert.Equal(true,resultData.response.shouldEndSession);

            Assert.Equal(1,resultData.response.directives.Count);

            var playDirective = resultData.response.directives[0];
            Assert.Equal("0",playDirective.audioItem.stream.token);
            Assert.Equal(0,playDirective.audioItem.stream.offsetInMilliseconds);
            Assert.Equal("https://alexabedtime.blob.core.windows.net/sounds/10-hours-rain-96bps.mp3", playDirective.audioItem.stream.url);

        }
    }
}
