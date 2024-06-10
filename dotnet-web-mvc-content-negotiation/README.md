# ASP.NET Core Content Negotiation

This sample builds upon “[Custom formatters in ASP.NET Core MVC web APIs](https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-formatters)” and its code sample [[GitHub](https://github.com/aspnet/Docs/tree/master/aspnetcore/mvc/advanced/custom-formatters/sample).] Here we add [integration tests](./Songhay.ContentNegotiation/Songhay.ContentNegotiation.Tests) to explicitly show how to make a request with `Content-Type` in the headers.

The `ContactControllerTest` [[class](./Songhay.ContentNegotiation/Songhay.ContentNegotiation.Tests/Controllers/ContactsControllerTest.cs)] should show that JSON is returned by default and [vCard](https://en.wikipedia.org/wiki/VCard) is returned per custom request. These expectations are made possible by _adding_ to `MvcOptions.OutputFormatters` instead of _inserting_ at position 0 (in [Startup.cs](./Songhay.ContentNegotiation/Songhay.ContentNegotiation/Startup.cs)):

```c#
services
    .AddSingleton<IContactRepository, ContactRepository>()
    .AddMvc(options =>
    {
        options.OutputFormatters.Add(new VcardOutputFormatter());
        //force vCard to be the default: options.OutputFormatters.Insert(0, new VcardOutputFormatter());
    });
```

## related links

* “[Content negotiation in ASPNET Core](https://dotnetthoughts.net/content-negotiation-in-aspnet-core/)”
* Historical, from 2014: “[Cutting Edge : Content Negotiation and Web API for the ASP.NET MVC Developer](https://msdn.microsoft.com/en-us/magazine/dn574797.aspx)”
* “[Difference between the two Web API headers response.Content.Headers and response.Headers](https://stackoverflow.com/questions/23209038/difference-between-the-two-web-api-headers-response-content-headers-and-response)”

🐙🐱[BryanWilhite](https://github.com/BryanWilhite)
