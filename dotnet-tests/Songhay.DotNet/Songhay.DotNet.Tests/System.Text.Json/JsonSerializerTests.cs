using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Songhay.DotNet.Tests.System.Text.Json
{
    public class JsonSerializerTests
    {
        public JsonSerializerTests(ITestOutputHelper helper)
        {
            this._testOutputHelper = helper;
        }

        [Fact]
        public void ShouldSerializeAnonymousObject()
        {
            string getElementJson()
            {
                var matchPropertyName = "myIndexNestedObject.myProperty";
                var matchPropertyExpectedValue = 99;

                using var stream = new MemoryStream();
                using var writer = new Utf8JsonWriter(stream);

                writer.WriteObject(() =>
                {
                    writer.WriteNumber(matchPropertyName, matchPropertyExpectedValue);
                });

                writer.Flush();

                string json = Encoding.UTF8.GetString(stream.ToArray());

                return json;
            }

            using var jDocument = JsonDocument.Parse(getElementJson());

            var anon = new
            {
                query = new
                {
                    nested = new
                    {
                        path = "myIndexNestedObject",
                        query = jDocument.RootElement
                    }
                }
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(anon, options);
            this._testOutputHelper.WriteLine(json);
        }

        [Fact]
        public void ShouldSerializeAnonymousObjectWithDictionary()
        {
            var matchPropertyName = "myIndexNestedObject.myProperty";
            var matchPropertyExpectedValue = 99;

            var anon = new
            {
                query = new
                {
                    nested = new
                    {
                        path = "myIndexNestedObject",
                        query = new Dictionary<string, int>
                        {
                            { matchPropertyName, matchPropertyExpectedValue }
                        }
                    }
                }
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(anon, options);
            this._testOutputHelper.WriteLine(json);
        }

        [Fact]
        public void ShouldSerializeAnonymousObjectWithDictionaryArray()
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

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(anon, options);
            this._testOutputHelper.WriteLine(json);
        }

        ITestOutputHelper _testOutputHelper;
    }
}