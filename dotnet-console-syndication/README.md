# `SyndicationFeedReaderWriter` on .NET Core

The `SyndicationFeedReaderWriter` [[GitHub](https://github.com/dotnet/SyndicationFeedReaderWriter)] is one of the many answers for the [current state](https://github.com/dotnet/wcf/issues/2098) of `System.ServiceModel.Syndication` on .NET Core. This sample follows [the one from Dody Gunawinata](https://github.com/dodyg/practical-aspnetcore/blob/master/projects/aspnet-core-2/syndication/src/Program.cs) without the use of ASP.NET Core. It starts with `Microsoft.SyndicationFeed.ReaderWriter` [[Nuget](https://www.nuget.org/packages/Microsoft.SyndicationFeed.ReaderWriter/)] from the sample [directory](../dotnet-console-syndication):

```bash
dotnet new console -o Songhay.SyndicationOne
dotnet add Songhay.SyndicationOne/Songhay.SyndicationOne.csproj package Microsoft.SyndicationFeed.ReaderWriter
```

Running `dotnet run` from the project directory should generate [an HTML representation](./Songhay.SyndicationOne/rss.html) of the RSS feed in the selfsame directory.

## related links

* “[Roadmap of SyndicationFeed #2098](https://github.com/dotnet/wcf/issues/2098)”
* “[Please add support for System.ServiceModel.Syndication #76](https://github.com/dotnet/wcf/issues/76)”

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
