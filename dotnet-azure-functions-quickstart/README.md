# Azure Functions “quickstart”

This is an obedient walk through “[Quickstart: Create a C# function in Azure using Visual Studio Code](https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp)” which can be useful for beginners (of course) and for the more experienced to see how Microsoft is introducing Azure Functions.

The only independent steps I am going to take are:

## generate the local project under my folder of choice

My folder of choice is the [`dotnet-azure-functions-quickstart`](../dotnet-azure-functions-quickstart) folder where the instructions under “[Create your local project](https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp?tabs=in-process#create-an-azure-functions-project)” are followed.

## install Azure Functions Core Tools on Ubuntu

From “[Install the Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=v4%2Clinux%2Ccsharp%2Cportal%2Cbash#tabpanel_2_linux_v4)”:

```bash
sudo apt-get install azure-functions-core-tools-4
```

## adding a `*.sln` file

A `*.sln` file is needed for the [OmniSharp](https://www.omnisharp.net/) **Select Project** command). From the `dotnet-azure-functions-quickstart/` [directory](../dotnet-azure-functions-quickstart):

```bash
dotnet new sln -n My.Functions

dotnet sln My.Functions.sln \
    add My.Functions/My.Functions.csproj
```

@[BryanWilhite](https://twitter.com/BryanWilhite)
