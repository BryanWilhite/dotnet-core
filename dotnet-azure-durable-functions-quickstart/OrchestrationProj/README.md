# Azure Durable Functions “quickstart”

This is an obedient walk through “[Quickstart: Create a C# function in Azure from the command line](https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-cli-csharp?tabs=linux%2Cazure-cli)” in a bash shell on Ubuntu 24.04.3 LTS. Because of this obedience Visual Studio Code is required with the following divergences:

1. ensure that Visual Studio Code generates the project in the `dotnet-azure-durable-functions-quickstart/OrchestrationProj` directory
2. rename the `*.csproj` file to `OrchestrationProj.csproj` in the `dotnet-azure-durable-functions-quickstart/OrchestrationProj` directory

From the `dotnet-azure-durable-functions-quickstart/` directory, run:

```bash
dotnet new sln -n Durable.Functions

dotnet sln Durable.Functions.sln \
    add OrchestrationProj/OrchestrationProj.csproj
```

…and use Visual Studio Code to select the new Solution file.

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
