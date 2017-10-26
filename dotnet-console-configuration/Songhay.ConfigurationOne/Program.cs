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

            //connection string convention:
            var cnnKey = "contosoOne";
            var cnn = configuration.GetConnectionString(cnnKey);
            Console.WriteLine($"{cnnKey}: {cnn}");

            //binding to a dictionary:
            var set = new Dictionary<string, string>();
            configuration.GetSection("myDictionary").Bind(set);
            foreach (var pair in set)
            {
                Console.WriteLine($"key: {pair.Key}, value: {pair.Value}");
            }

            //system environment variable:
            var envKey = "PATH";
            var path = configuration[envKey];
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine($"{envKey}: [not found on this system]");
            }
            else
            {
                Console.WriteLine($"{envKey}: {path}");
            }
        }
    }
}