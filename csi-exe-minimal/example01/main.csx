#r ".\packages\Newtonsoft.Json.11.0.2\lib\net45\Newtonsoft.Json.dll"

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var data = new
{
    PropertyC = "three",
    PropertyD = "four",
    PropertyB = "two",
    PropertyA = "one",
};

var jO = JObject.FromObject(data);
Console.WriteLine("sorted");
Console.WriteLine(jO.ToString());

var jProperties = jO.Properties();
Console.WriteLine("IEnumerable<jProperty>");
foreach (JProperty p in jProperties) Console.WriteLine($"{p.Value}");

jO = new JObject(jProperties.OrderBy(i => i.Name));
Console.WriteLine("sorted");
Console.WriteLine(jO.ToString());