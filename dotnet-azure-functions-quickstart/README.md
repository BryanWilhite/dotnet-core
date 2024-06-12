# Azure Functions “quickstart”

This is an obedient walk through “[Quickstart: Create a C# function in Azure from the command line](https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-cli-csharp?tabs=linux%2Cazure-cli)” in a bash shell on Ubuntu 22.04.4 LTS.

The following table summarizes the command-line tools required:

| command | description |
| - | - |
| `dotnet` | The .NET CLI |
| `az` | The Azure CLI |
| `func` | Azure Functions Core Tools |
| `curl` | A tool for transferring data from or to a server. |

## generating a local function project

From the `/dotnet-azure-functions-quickstart` [directory](../dotnet-azure-functions-quickstart):

```bash
func init LocalFunctionProj \
    --worker-runtime dotnet-isolated \
    --target-framework net8.0
```

## adding a `*.sln` file

A `*.sln` file is needed for the [OmniSharp](https://www.omnisharp.net/) **Select Project** command) in Visual Studio Code. From the `dotnet-azure-functions-quickstart/` [directory](../dotnet-azure-functions-quickstart):

```bash
dotnet new sln -n Local.Functions

dotnet sln Local.Functions.sln \
    add LocalFunctionProj/LocalFunctionProj.csproj
```

## add a function to the project

From the `/dotnet-azure-functions-quickstart/LocalFunctionProj` [directory](../dotnet-azure-functions-quickstart/LocalFunctionProj):

```bash
func new --name HttpExample --template "HTTP trigger" --authlevel "anonymous"
```

## run the function locally


From the `/dotnet-azure-functions-quickstart/LocalFunctionProj` [directory](../dotnet-azure-functions-quickstart/LocalFunctionProj):

```bash
func start
```

## using `curl` to test against the local server

The following `curl` command is a lightweight way to hit breakpoints during the F5, debugging experience:

```shell
$ curl http://localhost:7071/api/HttpExample

Welcome to Azure Functions!

```

[Bryan Wilhite is on LinkedIn](https://www.linkedin.com/in/wilhite)🇺🇸💼
