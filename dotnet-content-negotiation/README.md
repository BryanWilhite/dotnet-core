# ASP.NET Core Content Negotiation

This sample builds upon “[Custom formatters in ASP.NET Core MVC web APIs](https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-formatters)” and its code sample [[GitHub](https://github.com/aspnet/Docs/tree/master/aspnetcore/mvc/advanced/custom-formatters/sample).] Here we add [integration tests](./Songhay.ContentNegotiation/Songhay.ContentNegotiation.Tests) to explicitly show how to make a request with `Content-Type` in the headers.

The `ContactControllerTest` [[class](./Songhay.ContentNegotiation/Songhay.ContentNegotiation.Tests/Controllers/ContactsControllerTest.cs)] should show that JSON is returned by default and vCard is returned per custom request. These expectations are made possible by _adding_ to `MvcOptions.OutputFormatters` instead of _inserting_ at position 0 (in [Startup.cs](./Songhay.ContentNegotiation/Songhay.ContentNegotiation/Startup.cs)):

```c#
services
    .AddSingleton<IContactRepository, ContactRepository>()
    .AddMvc(options =>
    {
        options.OutputFormatters.Add(new VcardOutputFormatter());
        //force vCard to be the default: options.OutputFormatters.Insert(0, new VcardOutputFormatter());
    });
```