using System.Collections.Generic;
using alexa_bedtime.function.Models;

namespace alexa_bedtime.function.Mappers
{
    public class AlexaResponseMapper
    {
        public AlexaResponse MapStopRequest(AlexaRequest request)
        {
            var response = new AlexaResponse
            {
                version = "1.1",
                response = new Response
                {
                    shouldEndSession = true,
                    card = new Card
                    {
                        type = "Simple",
                        title = "Bedtime",
                        content = "Enjoy your day!"
                    },
                    directives = new List<Directive>
                    {
                        new Directive
                        {
                            type = "AudioPlayer.Stop"
                        }
                    }
                }
            };

            return response;
        }

        public AlexaResponse MapPlaybackResponse(AlexaRequest request)
        {
            var response = new AlexaResponse
            {
                version = "1.0",
                response = new Response
                {
                    shouldEndSession = true,
                    directives = new List<Directive> 
                    { 
                        new Directive
                        {
                            type = "AudioPlayer.Play",
                            playBehavior = "REPLACE_ALL",
                            audioItem = new AudioItem
                            {
                                stream = new AudioStream
                                {
                                    token = "0",
                                    offsetInMilliseconds = 0,
                                    url = "https://alexabedtime.blob.core.windows.net/sounds/10-hours-rain-96bps.mp3"
        }
                            }
                        }
                    }
                }
            };

            return response;
        }
    }
}
