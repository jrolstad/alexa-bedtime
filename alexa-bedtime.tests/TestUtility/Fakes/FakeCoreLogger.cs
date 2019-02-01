using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;

namespace alexa_bedtime.tests.TestUtility.Fakes
{
    public class FakeCoreLogger:ILogger
    {
        private readonly TestContext _context;

        public FakeCoreLogger(TestContext context)
        {
            _context = context;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = new FakeLogMessage
            {
                Level = logLevel,
                Exception = exception,
                Message = formatter(state, exception),
                Data = state
            };
           
            _context.LogMessages.Add(message);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new DisposableObject();
        }

        public class DisposableObject : IDisposable
        {
            public void Dispose()
            {

            }
        }
    }

    public class FakeLogMessage
    {
        public LogLevel Level { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}