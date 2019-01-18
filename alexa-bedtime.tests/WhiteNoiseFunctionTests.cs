using System;
using System.IO;
using System.Linq;
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
        [Theory]
        [InlineData("whitenoise-startplayback-request.json")]
        public async Task Run_StartIntent_ReturnsAudioPlayerResult(string requestFile)
        {
            // Arrange
            var root = TestCompositionRoot.Create();

            var requestData = root.WithInputFile($"alexa_bedtime.tests.TestData.Requests.{requestFile}");
            var request = root.WithPostRequest(requestData);

            var logger = root.Get<ILogger>();
            
            // Act
            var result =  await WhiteNoiseFunction.Run(request, logger);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var resultData = result.Value<dynamic>();

            Assert.Equal("1.0",resultData.version);
            Assert.Equal(true,resultData.response.shouldEndSession);

            Assert.Equal(1,resultData.response.directives.Count);

            var playDirective = resultData.response.directives[0];
            Assert.Equal("AudioPlayer.Play", playDirective.type);
            Assert.Equal("REPLACE_ALL",playDirective.playBehavior);

            Assert.Equal("0",playDirective.audioItem.stream.token);
            Assert.Equal(0,playDirective.audioItem.stream.offsetInMilliseconds);
            Assert.Equal("https://alexabedtime.blob.core.windows.net/sounds/10-hours-rain-96bps.mp3", playDirective.audioItem.stream.url);

            AssertRequestIsLogged(root, requestData);
        }

        [Theory]
        [InlineData("whitenoise-stopplayback-request.json")]
        public async Task Run_StopIntent_ReturnsStopResult(string requestFile)
        {
            // Arrange
            var root = TestCompositionRoot.Create();

            var requestData = root.WithInputFile($"alexa_bedtime.tests.TestData.Requests.{requestFile}");
            var request = root.WithPostRequest(requestData);

            var logger = root.Get<ILogger>();

            // Act
            var result = await WhiteNoiseFunction.Run(request, logger);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var resultData = result.Value<dynamic>();

            Assert.Equal("1.1", resultData.version);
            Assert.Equal(true, resultData.response.shouldEndSession);

            Assert.Equal("Simple",resultData.response.card.type);
            Assert.Equal("Bedtime",resultData.response.card.title);
            Assert.Equal("Enjoy your day!",resultData.response.card.content);

        }

        private void AssertRequestIsLogged(TestCompositionRoot root, Stream request)
        {
            var reader = new StreamReader(request);
            var requestData = reader.ReadToEnd();
            var requestMessages = root
                .Context
                .LogMessages
                .Count(m => m.Message.Contains(requestData));

            Assert.Equal(1,requestMessages);
        }
    }
}
