using System;
using System.Collections.Generic;

namespace alexa_bedtime.function.Models
{
    public class Application
    {
        public string applicationId { get; set; }
    }

    public class User
    {
        public string userId { get; set; }
    }

    public class Session
    {
        public bool @new { get; set; }
        public string sessionId { get; set; }
        public Application application { get; set; }
        public User user { get; set; }
    }

    public class AudioPlayer
    {
        public int offsetInMilliseconds { get; set; }
        public string token { get; set; }
        public string playerActivity { get; set; }
    }



    public class SupportedInterfaces
    {
        public AudioPlayer AudioPlayer { get; set; }
    }

    public class Device
    {
        public string deviceId { get; set; }
        public SupportedInterfaces supportedInterfaces { get; set; }
    }

    public class System
    {
        public Application application { get; set; }
        public User user { get; set; }
        public Device device { get; set; }
        public string apiEndpoint { get; set; }
        public string apiAccessToken { get; set; }
    }

    public class Experience
    {
        public int arcMinuteWidth { get; set; }
        public int arcMinuteHeight { get; set; }
        public bool canRotate { get; set; }
        public bool canResize { get; set; }
    }

    public class Viewport
    {
        public List<Experience> experiences { get; set; }
        public string shape { get; set; }
        public int pixelWidth { get; set; }
        public int pixelHeight { get; set; }
        public int dpi { get; set; }
        public int currentPixelWidth { get; set; }
        public int currentPixelHeight { get; set; }
        public List<string> touch { get; set; }
        public List<object> keyboard { get; set; }
    }

    public class Context
    {
        public AudioPlayer AudioPlayer { get; set; }
        public System System { get; set; }
        public Viewport Viewport { get; set; }
    }

    public class Intent
    {
        public string name { get; set; }
        public string confirmationStatus { get; set; }
    }

    public class Request
    {
        public string type { get; set; }
        public string requestId { get; set; }
        public DateTime timestamp { get; set; }
        public string locale { get; set; }
        public Intent intent { get; set; }
    }

    public class AlexaRequest
    {
        public string version { get; set; }
        public Session session { get; set; }
        public Context context { get; set; }
        public Request request { get; set; }

        public string raw { get; set; }
    }
}
