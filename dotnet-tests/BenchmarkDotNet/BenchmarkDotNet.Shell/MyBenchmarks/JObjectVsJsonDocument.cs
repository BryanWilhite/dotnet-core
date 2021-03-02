using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace MyBenchmarks
{
    public class JObjectVsJsonDocument
    {
        [Benchmark]
        public void Serialize()
        {
            var o = GetObject();
            System.Text.Json.JsonSerializer.Serialize(o);
        }

        [Benchmark]
        public void SerializeNewtonsoft()
        {
            var o = GetObject();
            Newtonsoft.Json.JsonConvert.SerializeObject(o);
        }

        static object GetObject()
        {
            KeyValuePair<string, object>[] getItems()
            {
                return new Dictionary<string, object>
                {
                    { "one", new { isThing = true } },
                    { "two", new { isOtherThing = true } },
                }
                .ToArray();
            }

            var anon = new
            {
                items = getItems()
            };

            return anon;
        }
    }
}