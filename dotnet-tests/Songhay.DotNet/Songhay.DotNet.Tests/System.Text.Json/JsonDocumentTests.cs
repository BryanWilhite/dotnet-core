using System.Text.Json;
using Xunit;

namespace Songhay.DotNet.Tests.System.Text.Json
{
    public class JsonDocumentTests
    {
        [Fact]
        public void ShouldParseArray()
        {
             using var jsonDocument = JsonDocument.Parse("[]");
             Assert.True(jsonDocument.RootElement.ValueKind == JsonValueKind.Array);
        }
    }
}