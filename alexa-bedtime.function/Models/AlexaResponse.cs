using System;
using System.Collections.Generic;

namespace alexa_bedtime.function.Models
{
    public class AlexaResponse
    {
        public string version { get; set; }
        public Response response { get; set; }
    }

    public class Response
    {
        public bool shouldEndSession { get; set; }
        public Card card { get; set; }
        public List<Directive> directives { get; set; }
    }

    public class Card
    {
        public string type { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }

    public class Directive
    {
        public string type { get; set; }
        public string playBehavior { get; set; }
        public AudioItem audioItem { get; set; }
    }

    public class AudioItem
    {
        public AudioStream stream { get; set; }
    }

    public class AudioStream
    {
        public string url { get; set; }
        public string token { get; set; }
        public int offsetInMilliseconds { get; set; }
    }
}
