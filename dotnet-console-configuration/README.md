# .NET Core Configuration

This sample serves as my introduction to configuration files on .NET Core. Configuration files are in XML [[NuGet](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Xml/)], JSON [[NuGet](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/)] _or_ any other format with a valid provider. The preference here is for JSON.

This sample was built from the sample [directory](../dotnet-console-configuration):

```bash
dotnet new console -o Songhay.ConfigurationOne
dotnet add Songhay.ConfigurationOne/Songhay.ConfigurationOne.csproj package Microsoft.Extensions.Configuration
dotnet add Songhay.ConfigurationOne/Songhay.ConfigurationOne.csproj package Microsoft.Extensions.Configuration.Binder
dotnet add Songhay.ConfigurationOne/Songhay.ConfigurationOne.csproj package Microsoft.Extensions.Configuration.EnvironmentVariables
dotnet add Songhay.ConfigurationOne/Songhay.ConfigurationOne.csproj package Microsoft.Extensions.Configuration.FileExtensions
dotnet add Songhay.ConfigurationOne/Songhay.ConfigurationOne.csproj package Microsoft.Extensions.Configuration.Json
dotnet add Songhay.ConfigurationOne/Songhay.ConfigurationOne.csproj package Microsoft.Extensions.Configuration.CommandLine
```

Configuration starts with `Microsoft.Extensions.Configuration` [[NuGet](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/)], its `ConfigurationBuilder` class. By calling `SetBasePath()` and `AddJsonFile()` at the start of our application, we are using `Microsoft.Extensions.Configuration.FileExtensions` and `Microsoft.Extensions.Configuration.Json`, respectively:

```c#
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("app-settings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);
```

Notice that the call to `AddEnvironmentVariables()` makes use of `Microsoft.Extensions.Configuration.EnvironmentVariables`.

We would need `Microsoft.Extensions.Configuration.Binder` to instantiate .NET class from all or portions of the settings file. This done by calling the `IConfiguration.Bind()` method. Before we can do any binding, we obtain `IConfigurationRoot` from the `ConfigurationBuilder` we saw above:

```c#
IConfigurationRoot configuration = builder.Build();
```

To see command line arguments being passed in configuration, from the `Songhay.ConfigurationOne` [directory](./Songhay.ConfigurationOne) run:

```bash
dotnet run -- key1="one" key2="two" /key3 "value of three" --key-four "value of four"
```

These arguments show that .NET Core supports different formats that can be parsed simultaneously.

We can see all of this unfold in the `Program.cs` [file](./Songhay.ConfigurationOne/Program.cs).

## related links

* “[Using strongly typed configuration in .NET Core console app](https://blogs.msdn.microsoft.com/fkaduk/2017/02/22/using-strongly-typed-configuration-in-net-core-console-app/)”
* [ConfigurationBuilder Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbuilder?view=dotnet-plat-ext-5.0)
* `IConfigurationRoot` Interface [[docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfigurationroot?view=dotnet-plat-ext-5.0)]
* `IConfiguration` Interface [[docs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-5.0)]
* `CommandLine` configuration provider [[docs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration?tabs=basicconfiguration#commandline-configuration-provider)]

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
