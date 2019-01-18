using System;
using System.Collections.Generic;
using alexa_bedtime.tests.TestUtility.Fakes;
using Microsoft.Extensions.Logging;

namespace alexa_bedtime.tests.TestUtility
{
    public class TestCompositionRoot
    {
        public TestContext Context = new TestContext();

        public static TestCompositionRoot Create()
        {
            return new TestCompositionRoot();
        }

        private TestCompositionRoot()
        {

        }

        public T Get<T>() where T : class
        {
            if (typeof(T) == typeof(ILogger))
            {
                return new FakeCoreLogger(this.Context) as T;
            }

            throw new ArgumentOutOfRangeException("T",typeof(T),"Resolution of type not implemented");
        }
    }

    public class TestContext
    {
        public List<FakeLogMessage> LogMessages = new List<FakeLogMessage>();
    }
}