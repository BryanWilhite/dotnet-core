using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Songhay.ConfigurationOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app-settings.json", optional : false, reloadOnChange : true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            //simple key-value pair:
            Console.WriteLine($"defaultTraceSourceName: {configuration["defaultTraceSourceName"]}");

            //binding to a dictionary:
            var set = new Dictionary<string, string>();
            configuration.GetSection("myDictionary").Bind(set);
            foreach (var pair in set)
            {
                Console.WriteLine($"key: {pair.Key}, value: {pair.Value}");
            }
        }
    }
}