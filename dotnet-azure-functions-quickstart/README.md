# Azure Functions “quickstart”

This is an obedient walk through “[Quickstart: Create a C# function in Azure using Visual Studio Code](https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp)” which can be useful for beginners (of course) and for the more experienced to see how Microsoft is introducing Azure Functions.

The only independent steps I am going to take are done to ensure there is a `*.sln` file (for the [OmniSharp](https://www.omnisharp.net/) **Select Project** command) associated with this “quickstart” (from the `dotnet-azure-functions-quickstart/` [directory](../dotnet-azure-functions-quickstart)):

```bash
dotnet new sln -n My.Functions

dotnet sln My.Functions.sln \
    add My.Functions/My.Functions.csproj
```

Finally, I need to make sure I “create a local Azure Functions project” in the `My.Functions/` [directory](./My.Functions).

## lack of .NET 5.x support

[Azure Functions Core Tools](https://github.com/Azure/azure-functions-core-tools) does not currently support .NET 5.x. I refuse to install an additional SDK on my Ubuntu Desktop where I prefer to run Azure Functions Core Tools exclusively. Here is [a comment from a Microsoft Program Manager](https://github.com/Azure/azure-functions-core-tools/issues/2294#issuecomment-729966103) that did not quite work out as _hoped_:

>We’re hoping to have a .NET 5 worker in preview by end of year.

I am also terrible at giving development time estimates.

@[BryanWilhite](https://twitter.com/BryanWilhite)
