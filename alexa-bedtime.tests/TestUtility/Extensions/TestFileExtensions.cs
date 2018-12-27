using System.IO;
using System.Text;

namespace alexa_bedtime.tests.TestUtility.Extensions
{
    public static class TestFileExtensions
    {
        public static Stream WithInputFile(this object source, string name)
        {
            var assembly = typeof(TestFileExtensions).Assembly;
            var stream = assembly.GetManifestResourceStream(name);

            return stream;
        }

        public static string ReadFile(this Stream stream)
        {
            if (stream == null)
                return null;

            stream.Position = 0;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}