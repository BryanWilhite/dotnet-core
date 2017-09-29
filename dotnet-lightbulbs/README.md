# dotnet light bulbs

This sample is meant illustrate my development skills to a hiring third party.

## setup with the dotnet CLI

After manually generating the folders in [the sample folder](../dotnet-lightbulbs), these commands were run in that context to define the Solution:

```bash
dotnet new sln -o Songhay.LightBulbs.sln
dotnet new csproj -o Songhay.LightBulbs/Songhay.LightBulbs.csproj
dotnet new csproj -o Songhay.LightBulbs.Models/Songhay.LightBulbs.Models.csproj
dotnet new csproj -o Songhay.LightBulbs.Tests/Songhay.LightBulbs.Tests.csproj
dotnet sln Songhay.LightBulbs.sln add **/*.csproj

dotnet add Songhay.LightBulbs/Songhay.LightBulbs.csproj reference Songhay.LightBulbs.Models/Songhay.LightBulbs.Models.csproj
dotnet add Songhay.LightBulbs.Tests/Songhay.LightBulbs.Tests.csproj reference Songhay.LightBulbs/Songhay.LightBulbs.csproj
dotnet add Songhay.LightBulbs.Tests/Songhay.LightBulbs.Tests.csproj reference Songhay.LightBulbs.Models/Songhay.LightBulbs.Models.csproj

sudo dotnet add Songhay.LightBulbs.Models/Songhay.LightBulbs.Models.csproj package MoreLinq
sudo dotnet add Songhay.LightBulbs.Tests/Songhay.LightBulbs.Tests.csproj package MoreLinq
sudo dotnet add Songhay.LightBulbs.Tests/Songhay.LightBulbs.Tests.csproj package Newtonsoft.Json
```

To run the tests:

```bash
dotnet test Songhay.LightBulbs.Tests/Songhay.LightBulbs.Tests.csproj
```

## related links

* `dotnet new` [[docs](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new?tabs=netcore2x)]
* `dotnet sln` [[docs](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-sln)]
* `dotnet-add reference` [[docs](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-add-reference)]

@[BryanWilhite](https://twitter.com/bryanwilhite)
