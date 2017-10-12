# ASP.NET Core SPA Services

This sample is based on the documentation from [the JavaScriptServices repository](https://github.com/aspnet/JavaScriptServices) of the ASP.NET team. We start with the standard `mvc` template:

```ps1
dotnet new mvc -o Songhay.SpaServicesOne
```

Add this line to the `_ViewImports.cshtml` [file](./Songhay.SpaServicesOne/Views/_ViewImports.cshtml):

```cshtml
@addTagHelper *, Microsoft.AspNetCore.SpaServices
```

Then we declare this Tag Helper in the `Index.cshtml` [file](./Songhay.SpaServicesOne/Views/Home/Index.cshtml):

```html
<div id="my-spa" asp-prerender-module="ClientApp/boot-server"></div>
```

This Tag Helper depends on the `boot-server.js` [file](./Songhay.SpaServicesOne/ClientApp/boot-server.js) by convention:

```javascript
var prerendering = require("aspnet-prerendering");

module.exports = prerendering.createServerRenderer(function(params) {
    return new Promise(function(resolve, reject) {
        var result =
            "<h1>Hello world!</h1>" +
            "<p>Current time in Node is: " +
            new Date() +
            "</p>" +
            "<p>Request path is: " +
            params.location.path +
            "</p>" +
            "<p>Absolute URL is: " +
            params.absoluteUrl +
            "</p>";

        resolve({ html: result });
    });
});
```

From the `Songhay.SpaServicesOne` [folder](./Songhay.SpaServicesOne), run:

```ps1
dotnet build
$env:ASPNETCORE_ENVIRONMENT = "Development"
dotnet run
```

## related links

* “[Server-side prerendering](https://github.com/aspnet/JavaScriptServices/tree/dev/src/Microsoft.AspNetCore.SpaServices#server-side-prerendering)”