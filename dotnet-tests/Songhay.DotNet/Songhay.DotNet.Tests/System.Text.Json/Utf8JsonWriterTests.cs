using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Songhay.DotNet.Tests.System.Text.Json
{
    public class Utf8JsonWriterTests
    {
        public Utf8JsonWriterTests(ITestOutputHelper helper)
        {
            this._testOutputHelper = helper;
        }

        [Fact]
        public void ShouldWriteEmptyJsonObject()
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);

            writer.WriteStartObject();
            writer.WriteEndObject();
            writer.Flush();

            string json = Encoding.UTF8.GetString(stream.ToArray());

            this._testOutputHelper.WriteLine(json);
            Assert.Equal("{}", json);
        }

        [Fact]
        public void ShouldWriteJsonObject()
        {
            const string match = "match";
            const string path = "path";
            const string nested = "nested";
            const string query = "query";

            var options = new JsonWriterOptions
            {
                Indented = true,
            };

            var pathPropertyExpectedValue = "myIndexNestedObject";

            var matchPropertyName = "myIndexNestedObject.myProperty";
            var matchPropertyExpectedValue = 99;

            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, options);

            writer.WriteObject(() =>
            {
                writer.WriteObject(query, () =>
                {
                    writer.WriteObject(nested, () =>
                    {
                        writer.WriteString(path, pathPropertyExpectedValue);

                        writer.WriteObject(query, () =>
                        {
                            writer.WriteObject(match, () =>
                            {
                                writer.WriteNumber(matchPropertyName, matchPropertyExpectedValue);
                            });
                        });
                    });
                });
            });

            writer.Flush();

            string json = Encoding.UTF8.GetString(stream.ToArray());
            using var jDocument = JsonDocument.Parse(json);

            this._testOutputHelper.WriteLine(json);

            var pathProperty = jDocument.RootElement
                .GetProperty(query)
                .GetProperty(nested)
                .GetProperty(path);

            Assert.Equal(pathProperty.GetString(), pathPropertyExpectedValue);

            var matchProperty = jDocument.RootElement
                .GetProperty(query)
                .GetProperty(nested)
                .GetProperty(query)
                .GetProperty(match)
                .GetProperty(matchPropertyName);

            Assert.Equal(matchProperty.GetInt32(), matchPropertyExpectedValue);
        }

        ITestOutputHelper _testOutputHelper;
    }

    public static class Utf8JsonWriterExtensions
    {
        public static Utf8JsonWriter WriteObject(this Utf8JsonWriter writer, Action writerAction)
        {
            if (writer == null) return writer;

            writer.WriteStartObject();
            writerAction?.Invoke();
            writer.WriteEndObject();

            return writer;
        }

        public static Utf8JsonWriter WriteObject(this Utf8JsonWriter writer, string propertyName, Action writerAction)
        {
            if (writer == null) return writer;
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            writer.WritePropertyName(propertyName);
            writer.WriteStartObject();
            writerAction?.Invoke();
            writer.WriteEndObject();

            return writer;
        }
    }
}