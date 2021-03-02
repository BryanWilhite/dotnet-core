using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace MyBenchmarks
{
    public class JObjectVsJsonDocument
    {
        [Benchmark]
        public void Parse()
        {
            var json = this.Serialize();

            System.Text.Json.JsonDocument.Parse(json);
        }

        [Benchmark]
        public void ParseNewtonsoft()
        {
            var json = this.Serialize();

            Newtonsoft.Json.Linq.JObject.Parse(json);
        }

        [Benchmark]
        public string Serialize()
        {
            var o = GetObject();
            var json = System.Text.Json.JsonSerializer.Serialize(o);

            return json;
        }

        [Benchmark]
        public string SerializeNewtonsoft()
        {
            var o = GetObject();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(o);

            return json;
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