# Azure Durable Functions “quickstart”

This is a walk through “[Quickstart: Create a C# Durable Functions app](https://learn.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-isolated-create-first-csharp?pivots=code-editor-vscode)” that eventually diverges away largely because I was unable to debug the project (in Visual Studio Code) in this rather crowded repo.

My steps are these:

1. ensure that Visual Studio Code generates the project in the `dotnet-azure-durable-functions-quickstart/OrchestrationProj` directory
2. rename the `*.csproj` file to `OrchestrationProj.csproj` in the `dotnet-azure-durable-functions-quickstart/OrchestrationProj` directory
3. from the `dotnet-azure-durable-functions-quickstart/` directory, run:

```bash
dotnet new sln -n Durable.Functions

dotnet sln Durable.Functions.sln \
    add OrchestrationProj/OrchestrationProj.csproj
```

…and use Visual Studio Code to select the new Solution file. This `*.sln` file should make debugging possible in Visual Studio or JetBrains Rider.

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
